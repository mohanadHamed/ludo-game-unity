using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.AddressablesHelpers
{
    public static class AddressableSpriteLoader
    {
        public static void LoadSprite(string spriteAddress, Action<Sprite> done)
        {
            // Use Addressables to load the sprite.
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(spriteAddress);

            handle.Completed += operation =>
            {
                if (operation.Status == AsyncOperationStatus.Succeeded)
                {
                    done(operation.Result);

                    // Release the handle to free up resources.
                    Addressables.Release(handle);
                }
                else
                {
                    Debug.LogError("Failed to load the sprite: " + spriteAddress);
                    done(null);
                }
            };
        }
    }
}
