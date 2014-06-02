using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using MACPortal.Extensions;
using MACPortal.Migrations;
using MACPortal.Models.Users;

namespace MACPortal.ViewModel
{
    public class AvatarVM
    {
        public AvatarVM Start(Employee employee, bool setDefaultValues = true)
        {
            UserID = employee.UserID;

            if (employee.AvatarHair != null) AvatarHair = (AvatarHair)employee.AvatarHair;
            if (setDefaultValues && AvatarHair == null)
            {
                AvatarHair = employee.Gender == Gender.Feminino ? ViewModel.AvatarHair.Hair1 : ViewModel.AvatarHair.Hair5;
            }

            if (employee.AvatarFace != null) AvatarFace = (AvatarFace)employee.AvatarFace;
            if (setDefaultValues && AvatarFace == null) AvatarFace = ViewModel.AvatarFace.Face1;

            if (employee.AvatarClothes != null) AvatarClothes = (AvatarClothes)employee.AvatarClothes;
            if (setDefaultValues && AvatarClothes == null) AvatarClothes = ViewModel.AvatarClothes.Clothes1;

            if (employee.AvatarEyes != null) AvatarEyes = (AvatarEyes)employee.AvatarEyes;
            if (setDefaultValues && AvatarEyes == null) AvatarEyes = ViewModel.AvatarEyes.Eyes1;

            if (employee.AvatarNose != null) AvatarNose = (AvatarNose)employee.AvatarNose;
            if (setDefaultValues && AvatarNose == null) AvatarNose = ViewModel.AvatarNose.Nose1;

            if (employee.AvatarEars != null) AvatarEars = (AvatarEars)employee.AvatarEars;
            if (setDefaultValues && AvatarEars == null) AvatarEars = ViewModel.AvatarEars.Ear1;

            if (employee.AvatarMouth != null) AvatarMouth = (AvatarMouth)employee.AvatarMouth;
            if (setDefaultValues && AvatarMouth == null) AvatarMouth = ViewModel.AvatarMouth.Mouth1;

            if (employee.AvatarAccessoryHead != null) AvatarAccessoryHead = (AvatarHeadAccessory)employee.AvatarAccessoryHead;
            if (setDefaultValues && AvatarAccessoryHead == null) AvatarAccessoryHead = AvatarHeadAccessory.HeadAccessory1;

            if (employee.AvatarAccessoryFace != null) AvatarAccessoryFace = (AvatarFaceAccessory)employee.AvatarAccessoryFace;
            if (setDefaultValues && AvatarAccessoryFace == null) AvatarAccessoryFace = AvatarFaceAccessory.HeadAccessory1;

            if (employee.AvatarAccessoryBody != null) AvatarAccessoryBody = (AvatarBodyAccessory)employee.AvatarAccessoryBody;
            if (setDefaultValues && AvatarAccessoryBody == null) AvatarAccessoryBody = AvatarBodyAccessory.BodyAccessory1;

            //Colors
            if (employee.AvatarSkinColor != null) AvatarSkinColor = (AvatarSkinColor)employee.AvatarSkinColor;
            if (setDefaultValues && (AvatarSkinColor == null || !AvatarSkinColor.IsDefined())) AvatarSkinColor = ViewModel.AvatarSkinColor.A1;

            if (employee.AvatarMouthColor != null) AvatarMouthColor = (AvatarBasicColor)employee.AvatarMouthColor;
            if (setDefaultValues && (AvatarMouthColor == null || !AvatarMouthColor.IsDefined())) AvatarMouthColor = AvatarBasicColor.A1;

            if (employee.AvatarEyesColor != null) AvatarEyesColor = (AvatarBasicColor)employee.AvatarEyesColor;
            if (setDefaultValues && (AvatarEyesColor == null || !AvatarEyesColor.IsDefined())) AvatarEyesColor = AvatarBasicColor.A1;

            if (employee.AvatarClothesColor != null) AvatarClothesColor = (AvatarBasicColor)employee.AvatarClothesColor;
            if (setDefaultValues && (AvatarClothesColor == null || !AvatarClothesColor.IsDefined())) AvatarClothesColor = AvatarBasicColor.A1;

            if (employee.AvatarHairColor != null) AvatarHairColor = (AvatarBasicColor)employee.AvatarHairColor;
            if (setDefaultValues && (AvatarHairColor == null || !AvatarHairColor.IsDefined())) AvatarHairColor = AvatarBasicColor.A1;

            return this;
        }

        public void Finish(Employee employee)
        {
            if (AvatarHair != null) employee.AvatarHair = (int)AvatarHair;
            else employee.AvatarHair = null;

            if (AvatarFace != null) employee.AvatarFace = (int)AvatarFace;
            else employee.AvatarFace = null;

            if (AvatarClothes != null) employee.AvatarClothes = (int)AvatarClothes;
            else employee.AvatarClothes = null;

            if (AvatarEyes != null) employee.AvatarEyes = (int)AvatarEyes;
            else employee.AvatarEyes = null;

            if (AvatarNose != null) employee.AvatarNose = (int)AvatarNose;
            else employee.AvatarNose = null;

            if (AvatarEars != null) employee.AvatarEars = (int)AvatarEars;
            else employee.AvatarEars = null;

            if (AvatarMouth != null) employee.AvatarMouth = (int)AvatarMouth;
            else employee.AvatarMouth = null;

            if (AvatarAccessoryHead != null) employee.AvatarAccessoryHead = (int)AvatarAccessoryHead;
            else employee.AvatarAccessoryHead = null;

            if (AvatarAccessoryFace != null) employee.AvatarAccessoryFace = (int)AvatarAccessoryFace;
            else employee.AvatarAccessoryFace = null;

            if (AvatarAccessoryBody != null) employee.AvatarAccessoryBody = (int)AvatarAccessoryBody;
            else employee.AvatarAccessoryBody = null;

            //Colors
            if (AvatarHairColor != null) employee.AvatarHairColor = (int)AvatarHairColor.Value;
            else employee.AvatarHairColor = null;

            if (AvatarSkinColor != null) employee.AvatarSkinColor = (int)AvatarSkinColor.Value;
            else employee.AvatarSkinColor = null;

            if (AvatarMouthColor != null) employee.AvatarMouthColor = (int)AvatarMouthColor.Value;
            else employee.AvatarMouthColor = null;

            if (AvatarEyesColor != null) employee.AvatarEyesColor = (int)AvatarEyesColor.Value;
            else employee.AvatarEyesColor = null;

            if (AvatarClothesColor != null) employee.AvatarClothesColor = (int)AvatarClothesColor.Value;
            else employee.AvatarClothesColor = null;
        }

        public int UserID { get; set; }

        [Display(Name = "Cabelo")]
        public AvatarHair? AvatarHair { get; set; }

        [Display(Name = "Cor do Cabelo")]
        public AvatarBasicColor? AvatarHairColor { get; set; }

        [Display(Name = "Cor da Pele")]
        public AvatarSkinColor? AvatarSkinColor { get; set; }

        [Display(Name = "Rosto")]
        public AvatarFace? AvatarFace { get; set; }

        [Display(Name = "Roupa")]
        public AvatarClothes? AvatarClothes { get; set; }

        [Display(Name = "Cor da Roupa")]
        public AvatarBasicColor? AvatarClothesColor { get; set; }

        [Display(Name = "Cor da Boca")]
        public AvatarBasicColor? AvatarMouthColor { get; set; }

        [Display(Name = "Olhos")]
        public AvatarEyes? AvatarEyes { get; set; }

        [Display(Name = "Cor dos Olhos")]
        public AvatarBasicColor? AvatarEyesColor { get; set; }

        [Display(Name = "Nariz")]
        public AvatarNose? AvatarNose { get; set; }

        [Display(Name = "Orelhas")]
        public AvatarEars? AvatarEars { get; set; }

        [Display(Name = "Boca")]
        public AvatarMouth? AvatarMouth { get; set; }

        [Display(Name = "Acessório Cabeça")]
        public AvatarHeadAccessory? AvatarAccessoryHead { get; set; }

        [Display(Name = "Acessório Rosto")]
        public AvatarFaceAccessory? AvatarAccessoryFace { get; set; }

        [Display(Name = "Acessório Corpo")]
        public AvatarBodyAccessory? AvatarAccessoryBody { get; set; }

        public string AvatarFile { get; set; }

    }



    public enum AvatarHair
    {
        [Display(Name = "Cabelo 1")]
        Hair1,
        [Display(Name = "Cabelo 2")]
        Hair2,
        [Display(Name = "Cabelo 3")]
        Hair3,
        [Display(Name = "Cabelo 4")]
        Hair4,
        [Display(Name = "Cabelo 5")]
        Hair5,
        [Display(Name = "Cabelo 6")]
        Hair6,
        [Display(Name = "Cabelo 7")]
        Hair7,
        [Display(Name = "Cabelo 8")]
        Hair8,
        [Display(Name = "Cabelo 9")]
        Hair9,
        [Display(Name = "Cabelo 10")]
        Hair10,
        [Display(Name = "Cabelo 11")]
        Hair11,
        [Display(Name = "Cabelo 12")]
        Hair12
    }

    public enum AvatarEyes
    {
        [Display(Name = "Olhos 1")]
        Eyes1,
        [Display(Name = "Olhos 2")]
        Eyes2,
        [Display(Name = "Olhos 3")]
        Eyes3,
        [Display(Name = "Olhos 4")]
        Eyes4,
        [Display(Name = "Olhos 5")]
        Eyes5,
        [Display(Name = "Olhos 6")]
        Eyes6,
        [Display(Name = "Olhos 7")]
        Eyes7,
        [Display(Name = "Olhos 8")]
        Eyes8,
        [Display(Name = "Olhos 9")]
        Eyes9,
        [Display(Name = "Olhos 10")]
        Eyes10,
        [Display(Name = "Olhos 11")]
        Eyes11
    }

    public enum AvatarMouth
    {
        [Display(Name = "Boca 1")]
        Mouth1,
        [Display(Name = "Boca 2")]
        Mouth2,
        [Display(Name = "Boca 3")]
        Mouth3,
        [Display(Name = "Boca 4")]
        Mouth4,
        [Display(Name = "Boca 5")]
        Mouth5,
        [Display(Name = "Boca 6")]
        Mouth6,
        [Display(Name = "Boca 7")]
        Mouth7,
        [Display(Name = "Boca 8")]
        Mouth8,
        [Display(Name = "Boca 9")]
        Mouth9,
        [Display(Name = "Boca 10")]
        Mouth10
    }

    public enum AvatarEars
    {
        [Display(Name = "Orelha 1")]
        Ear1,
        [Display(Name = "Orelha 2")]
        Ear2,
        [Display(Name = "Orelha 3")]
        Ear3,
        [Display(Name = "Orelha 4")]
        Ear4,
        [Display(Name = "Orelha 5")]
        Ear5,
        [Display(Name = "Orelha 6")]
        Ear6,
        [Display(Name = "Orelha 7")]
        Ear7
    }

    public enum AvatarClothes
    {
        [Display(Name = "Roupa 1")]
        Clothes1,
        [Display(Name = "Roupa 2")]
        Clothes2,
        [Display(Name = "Roupa 3")]
        Clothes3,
        [Display(Name = "Roupa 4")]
        Clothes4,
        [Display(Name = "Roupa 5")]
        Clothes5,
        [Display(Name = "Roupa 6")]
        Clothes6,
        [Display(Name = "Roupa 7")]
        Clothes7,
        [Display(Name = "Roupa 8")]
        Clothes8,
        [Display(Name = "Roupa 9")]
        Clothes9
    }

    public enum AvatarNose
    {
        [Display(Name = "Nariz 1")]
        Nose1,
        [Display(Name = "Nariz 2")]
        Nose2,
        [Display(Name = "Nariz 3")]
        Nose3,
        [Display(Name = "Nariz 4")]
        Nose4,
        [Display(Name = "Nariz 5")]
        Nose5,
        [Display(Name = "Nariz 6")]
        Nose6,
        [Display(Name = "Nariz 7")]
        Nose7,
        [Display(Name = "Nariz 8")]
        Nose8,
        [Display(Name = "Nariz 9")]
        Nose9
    }

    public enum AvatarHeadAccessory
    {
        [Display(Name = "Nenhum")]
        HeadAccessory1,
        [Display(Name = "Boné Azul")]
        HeadAccessory2,
        [Display(Name = "Boné Coração")]
        HeadAccessory3,
        [Display(Name = "Coroa")]
        HeadAccessory4,
        [Display(Name = "Tiara")]
        HeadAccessory5,
        [Display(Name = "Chapéu Asia")]
        HeadAccessory6,
        [Display(Name = "Chapéu Preto")]
        HeadAccessory7
    }

    public enum AvatarFaceAccessory
    {
        [Display(Name = "Nenhum")]
        HeadAccessory1,
        [Display(Name = "Brincos")]
        HeadAccessory2,
        [Display(Name = "Óculos Rosa")]
        HeadAccessory3,
        [Display(Name = "Óculos Preto")]
        HeadAccessory4,
        [Display(Name = "Blush 1")]
        HeadAccessory5,
        [Display(Name = "Blush 2")]
        HeadAccessory6,
        [Display(Name = "Óculos Bola")]
        HeadAccessory7,
        [Display(Name = "Máscara 1")]
        HeadAccessory8,
        [Display(Name = "Máscara 2")]
        HeadAccessory9
    }

    public enum AvatarBodyAccessory
    {
        [Display(Name = "Nenhum")]
        BodyAccessory1,
        [Display(Name = "Crachá Rosa")]
        BodyAccessory2,
        [Display(Name = "Crachá Azul")]
        HeadAccessory3,
        [Display(Name = "Corrente")]
        HeadAccessory4,
        [Display(Name = "Colar")]
        HeadAccessory5,
        [Display(Name = "Cachecol 1")]
        HeadAccessory6,
        [Display(Name = "Cachecol 2")]
        HeadAccessory7
    }

    public enum AvatarFace
    {
        [Display(Name = "Rosto 1")]
        Face1,
        [Display(Name = "Rosto 2")]
        Face2,
        [Display(Name = "Rosto 3")]
        Face3,
        [Display(Name = "Rosto 4")]
        Face4,
        [Display(Name = "Rosto 5")]
        Face5,
        [Display(Name = "Rosto 6")]
        Face6,
        [Display(Name = "Rosto 7")]
        Face7,
        [Display(Name = "Rosto 8")]
        Face8
    }

    public enum AvatarSkinColor
    {
        [Display(Name = "f5d4cd", Order = 1)]
        A1 = 0xf5d4cd,
        [Display(Name = "e0b0a6", Order = 4)]
        A2 = 0xe0b0a6,
        [Display(Name = "c68d82", Order = 7)]
        A3 = 0xc68d82,
        [Display(Name = "a36a5f", Order = 10)]
        A4 = 0xa36a5f,
        [Display(Name = "7b4b41", Order = 13)]
        A5 = 0x7b4b41,
        [Display(Name = "4e2f2a", Order = 16)]
        A6 = 0x4e2f2a,

        [Display(Name = "eed7c7", Order = 2)]
        B1 = 0xeed7c7,
        [Display(Name = "d9b59b", Order = 5)]
        B2 = 0xd9b59b,
        [Display(Name = "bd9174", Order = 8)]
        B3 = 0xbd9174,
        [Display(Name = "9a7153", Order = 11)]
        B4 = 0x9a7153,
        [Display(Name = "745036", Order = 14)]
        B5 = 0x745036,
        [Display(Name = "483322", Order = 17)]
        B6 = 0x483322,

        [Display(Name = "ead8c4", Order = 3)]
        C1 = 0xead8c4,
        [Display(Name = "d2b897", Order = 6)]
        C2 = 0xd2b897,
        [Display(Name = "b4976f", Order = 9)]
        C3 = 0xb4976f,
        [Display(Name = "92754d", Order = 12)]
        C4 = 0x92754d,
        [Display(Name = "6d5533", Order = 15)]
        C5 = 0x6d5533,
        [Display(Name = "453420", Order = 18)]
        C6 = 0x453420
    }

    public enum AvatarBasicColor
    {
        [Display(Name = "121415", Order = 1)]
        A1 = 0x121415,
        [Display(Name = "422824", Order = 2)]
        A2 = 0x422824,
        [Display(Name = "7a6137", Order = 3)]
        A3 = 0x7a6137,

        [Display(Name = "c3b912", Order = 4)]
        A4 = 0xc3b912,
        [Display(Name = "FEDD00", Order = 5)]
        A5 = 0xFEDD00,
        [Display(Name = "fff08f", Order = 6)]
        A6 = 0xfff08f,

        [Display(Name = "690e12", Order = 7)]
        B1 = 0x690e12,
        [Display(Name = "BB2026", Order = 8)]
        B2 = 0xBB2026,
        [Display(Name = "cd595e", Order = 9)]
        B3 = 0xcd595e,

        [Display(Name = "12497c", Order = 10)]
        B4 = 0x12497c,
        [Display(Name = "3e4095", Order = 11)]
        B5 = 0x3e4095,
        [Display(Name = "9bc8f1", Order = 12)]
        B6 = 0x9bc8f1,

        [Display(Name = "056429", Order = 13)]
        C1 = 0x056429,
        [Display(Name = "009916", Order = 14)]
        C2 = 0x009916,
        [Display(Name = "90e177", Order = 15)]
        C3 = 0x90e177,

        [Display(Name = "490A3D", Order = 16)]
        C4 = 0x490A3D,
        [Display(Name = "E97F02", Order = 17)]
        C5 = 0xE97F02,
        [Display(Name = "D9CEB2", Order = 18)]
        C6 = 0xD9CEB2
    }
}