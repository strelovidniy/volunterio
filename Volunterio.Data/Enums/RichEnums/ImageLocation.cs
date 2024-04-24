﻿using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class ImageLocation : RichEnum<string>
{
    private const string Base = "Volunterio.Server.Views.Images.";

    public static ImageLocation Icon => new($"{Base}Logo.svg");

    public static ImageLocation TwitterIcon => new($"{Base}Twitter.png");

    public static ImageLocation FacebookIcon => new($"{Base}Facebook.png");

    public static ImageLocation InstagramIcon => new($"{Base}Instagram.png");

    public static ImageLocation WhiteBackground => new($"{Base}WhiteBackground.png");

    public static ImageLocation Divider => new($"{Base}Divider.png");

    private ImageLocation(string value) : base(value)
    {
    }
}