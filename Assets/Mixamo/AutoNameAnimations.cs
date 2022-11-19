using UnityEditor;
using UnityEngine;

public class AutoNameAnimations : AssetPostprocessor {

    static readonly string[] renameMatches = new string[] { "Take 001", "mixamo.com" };

    void OnPostprocessModel(GameObject gameObject) {

        ModelImporter importer = assetImporter as ModelImporter;

        //importer.clipAnimations wil be 0 if its using the default clip animations, 
        //if there has been manual edits or edits by this script the length wont be 0
        if (importer.clipAnimations.Length == 0) {

            ModelImporterClipAnimation[] clipAnimations = importer.defaultClipAnimations;
            bool useSuffix = importer.defaultClipAnimations.Length > 1;
            bool reimportRequired = false;

            for (int i = 0; i < clipAnimations.Length; i++) {
                for (int j = 0; j < renameMatches.Length; j++) {
                    if (clipAnimations[i].takeName == renameMatches[j] || clipAnimations[i].name == renameMatches[j]) {
                        string newAnimationName = gameObject.name + (useSuffix ? "_" + j.ToString() : "");
                        clipAnimations[i].takeName = clipAnimations[i].name = newAnimationName;
                        reimportRequired = true;
                    }
                }
            }

            if (reimportRequired) {
                importer.clipAnimations = clipAnimations;
                importer.SaveAndReimport();
            }
        }
    }
}