using DiskCardGame;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace InscryptionAPI.Card
{
    public static class AnimatedCardExtensions
    {
        private const string AnimatedCardPortraitPrefab = "Prefabs/Cards/AnimatedPortraits/TalkingCardPortrait";

        private static readonly CharacterFace CharacterFaceBase = ResourceBank.Get<CharacterFace>(AnimatedCardPortraitPrefab);

        public static CardInfo MakeAnimated(this CardInfo info, GameObject animatedPortrait)
        {
            if (!info.appearanceBehaviour.Contains(CardAppearanceBehaviour.Appearance.AnimatedPortrait)) {
                info.appearanceBehaviour.Add(CardAppearanceBehaviour.Appearance.AnimatedPortrait);
            }
            info.animatedPortrait = animatedPortrait;
            return info;
        }

        public static GameObject CreateTalkingCardAnimation(
            List<CharacterFace.EmotionSprites> emotionSpritesList,
            float blinkRate = 3f
        )
        {
            CharacterFace characterFace = CharacterFaceBase;
            characterFace.eyes.blinkRate = blinkRate;
            characterFace.emotionSprites = emotionSpritesList;
            return characterFace.gameObject;
        }

        public static GameObject CreateAnimatedCard(Emotion emotion = Emotion.Neutral, string facePng = null,
            string eyesOpenPng = null, string eyesClosedPng = null,
            string eyesOpenEmissionPng = null, string eyesClosedEmissionPng = null,
            string mouthOpenPng = null, string mouthClosedPng = null,
            float blinkRate = 3f)
        {
            return CreateTalkingCardAnimation(
                new List<CharacterFace.EmotionSprites>()
                { CreateSpritesForEmotion(emotion, facePng, eyesOpenPng, eyesClosedPng,
                    eyesOpenEmissionPng, eyesClosedEmissionPng, mouthOpenPng, mouthClosedPng)
                },
                blinkRate
            );
        }

        public static CharacterFace.EmotionSprites CreateSpritesForEmotion(
            Emotion emotion = Emotion.Neutral, string facePng = null,
            string eyesOpenPng = null, string eyesClosedPng = null,
            string eyesOpenEmissionPng = null, string eyesClosedEmissionPng = null,
            string mouthOpenPng = null, string mouthClosedPng = null
        )
        {
            return new CharacterFace.EmotionSprites()
            {
                emotion = emotion,
                face = TextureHelper.GetImageAsSprite(facePng, new Vector2(0.5f, 0)),
                eyesOpen = TextureHelper.GetImageAsSprite(eyesOpenPng),
                eyesClosed = TextureHelper.GetImageAsSprite(eyesClosedPng),
                eyesOpenEmission = TextureHelper.GetImageAsSprite(eyesOpenEmissionPng),
                eyesClosedEmission = TextureHelper.GetImageAsSprite(eyesClosedEmissionPng),
                mouthOpen = TextureHelper.GetImageAsSprite(mouthOpenPng),
                mouthClosed = TextureHelper.GetImageAsSprite(mouthClosedPng),
            };
        }
    }
}
