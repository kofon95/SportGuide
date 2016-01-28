using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utils
{
    public static class Consts
    {
        public static class UrlPaths
        {
            public const string GlobalSource = "/source/";
            public const string TrainerImageSource = GlobalSource + "img-data/";
            public const string HallImageSource = GlobalSource + "img-data/";
        }
        public static class FilePaths
        {
            public const string GlobalSource = "~/source/";
            public const string TrainerImageSource = GlobalSource + "img-data/";
            public const string HallImageSource = GlobalSource + "img-data/";
        }


        /// <summary>Length as bytes</summary>
        public static class ContentLengths
        {
            public const int Hall_MaxWidthImage = 1920;
            public const int Hall_MaxHeightImage = 1280;

            public const int Trainer_MaxWidthImage = 1920;
            public const int Trainer_MaxHeightImage = 1280;
        }
    }
}