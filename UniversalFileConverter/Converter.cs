using System.Collections.Generic;
using System.Diagnostics;

namespace UniversalFileConverter
{
    public enum FileType
    {
        Audio,
        Video,
        Image,
        Unknown,
    }

    public static class Converter
    {
        public static readonly Dictionary<FileType, string[]> FilesFormats = new()
        {
            [FileType.Image] = new string[]
            {
                "a64",
                "apng",
                "bmp",
                "fits",
                "gif",
                "jpg",
                "jpeg",
                "png",
                "tif",
                "tiff",
                "webp",
            },

            [FileType.Audio] = new string[]
            {
                "aac",
                "ac3",
                "adts",
                "aif",
                "aiff",
                "apm",
                "au",
                "caf",
                "eac3",
                "flac",
                "latm",
                "m4a",
                "mka",
                "mp2",
                "mp3",
                "oga",
                "ogg",
                "opus",
                "sox",
                "spx",
                "tta",
                "voc",
                "w64",
                "wav",
                "wma",
                "wv",
            },

            [FileType.Video] = new string[]
            {
                "264",
                "asf",
                "avi",
                "f4v",
                "flv",
                "h264",
                "hevc",
                "ismv",
                "ivf",
                "m4v",
                "mjpeg",
                "mkv",
                "mov",
                "mp4",
                "mpeg",
                "nut",
                "ogv",
                "psp",
                "rm",
                "swf",
                "vob",
                "webm",
                "wtv",

                "aac",
                "ac3",
                "adts",
                "aif",
                "aiff",
                "apm",
                "au",
                "caf",
                "eac3",
                "flac",
                "latm",
                "m4a",
                "mka",
                "mp2",
                "mp3",
                "oga",
                "ogg",
                "opus",
                "sox",
                "spx",
                "tta",
                "voc",
                "w64",
                "wav",
                "wma",
                "wv",
            },

            [FileType.Unknown] = System.Array.Empty<string>(),
        };

        public static FileType GetFileType(string file) => file[file.LastIndexOf('.')..].ToLower() switch
        {
            (".a64") => FileType.Image,
            (".anm") => FileType.Image,
            (".apm") => FileType.Image,
            (".apng") => FileType.Image,
            (".bmp") => FileType.Image,
            (".dsf") => FileType.Image,
            (".fits") => FileType.Image,
            (".gif") => FileType.Image,
            (".iss") => FileType.Image,
            (".jfif") => FileType.Image,
            (".jpg") => FileType.Image,
            (".jpeg") => FileType.Image,
            (".lxf") => FileType.Image,
            (".png") => FileType.Image,
            (".tif") => FileType.Image,
            (".tiff") => FileType.Image,
            (".webp") => FileType.Image,


            (".aa") => FileType.Audio,
            (".aac") => FileType.Audio,
            (".aax") => FileType.Audio,
            (".ac3") => FileType.Audio,
            (".acm") => FileType.Audio,
            (".act") => FileType.Audio,
            (".adts") => FileType.Audio,
            (".afc") => FileType.Audio,
            (".aif") => FileType.Audio,
            (".aiff") => FileType.Audio,
            (".au") => FileType.Audio,
            (".avr") => FileType.Audio,
            (".brstm") => FileType.Audio,
            (".caf") => FileType.Audio,
            (".cdg") => FileType.Audio,
            (".dss") => FileType.Audio,
            (".dtshd") => FileType.Audio,
            (".ea") => FileType.Audio,
            (".eac3") => FileType.Audio,
            (".flac") => FileType.Audio,
            (".fsb") => FileType.Audio,
            (".hcom") => FileType.Audio,
            (".iff") => FileType.Audio,
            (".ladt") => FileType.Audio,
            (".m4a") => FileType.Audio,
            (".mka") => FileType.Audio,
            (".mp2") => FileType.Audio,
            (".mp3") => FileType.Audio,
            (".mpc") => FileType.Audio,
            (".oga") => FileType.Audio,
            (".ogg") => FileType.Audio,
            (".opus") => FileType.Audio,
            (".qcp") => FileType.Audio,
            (".sbg") => FileType.Audio,
            (".sds") => FileType.Audio,
            (".sdx") => FileType.Audio,
            (".shn") => FileType.Audio,
            (".sox") => FileType.Audio,
            (".spx") => FileType.Audio,
            (".sup") => FileType.Image,
            (".tak") => FileType.Audio,
            (".tta") => FileType.Audio,
            (".vag") => FileType.Audio,
            (".vmd") => FileType.Audio,
            (".voc") => FileType.Audio,
            (".vqf") => FileType.Audio,
            (".w64") => FileType.Audio,
            (".wav") => FileType.Audio,
            (".wma") => FileType.Audio,
            (".wv") => FileType.Audio,
            (".xa") => FileType.Audio,
            (".xwma") => FileType.Audio,


            (".264") => FileType.Video,
            (".3g2") => FileType.Video,
            (".3gp") => FileType.Video,
            (".amv") => FileType.Video,
            (".asf") => FileType.Video,
            (".avi") => FileType.Video,
            (".avs") => FileType.Video,
            (".cdxl") => FileType.Video,
            (".cine") => FileType.Video,
            (".dv") => FileType.Video,
            (".dxa") => FileType.Video,
            (".f4v") => FileType.Video,
            (".flic") => FileType.Video,
            (".flv") => FileType.Video,
            (".h261") => FileType.Video,
            (".h263") => FileType.Video,
            (".h264") => FileType.Video,
            (".hevc") => FileType.Video,
            (".ifv") => FileType.Video,
            (".ismv") => FileType.Video,
            (".ivf") => FileType.Video,
            (".ivr") => FileType.Video,
            (".kux") => FileType.Video,
            (".m4v") => FileType.Video,
            (".mjpeg") => FileType.Video,
            (".mkv") => FileType.Video,
            (".mov") => FileType.Video,
            (".mp4") => FileType.Video,
            (".mpeg") => FileType.Video,
            (".mpsub") => FileType.Video,
            (".mtv") => FileType.Video,
            (".mxf") => FileType.Video,
            (".nsv") => FileType.Video,
            (".nut") => FileType.Video,
            (".nuv") => FileType.Video,
            (".ovg") => FileType.Video,
            (".pjs") => FileType.Video,
            (".psp") => FileType.Video,
            (".pva") => FileType.Video,
            (".r3d") => FileType.Video,
            (".rm") => FileType.Video,
            (".roq") => FileType.Video,
            (".sdr2") => FileType.Video,
            (".smk") => FileType.Video,
            (".swf") => FileType.Video,
            (".vivo") => FileType.Video,
            (".vob") => FileType.Video,
            (".webm") => FileType.Video,
            (".wtv") => FileType.Video,
            (".xmv") => FileType.Video,
            _ => FileType.Unknown
        };

        /// <summary>
        /// Changes exctension of file path
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="extension">New extension</param>
        /// <returns></returns>
        public static string ChageExtension(string file, string extension)
        {
            return file.Remove(file.LastIndexOf('.')) + "." + extension;
        }

        /// <summary>
        /// Converts file to a specific format
        /// </summary>
        /// <param name="file">Input file path</param>
        /// <param name="format">Output file format</param>
        public static Process Convert(string from, string to, bool overwrite = false)
        {
            string ov;
            if (overwrite)
                ov = "-y";
            else
                ov = "-n";
            Process conv = new()
            {
                StartInfo = new()
                {
                    FileName = "ffmpeg.exe",
                    Arguments = $"-i \"{from}\" \"{to}\" -loglevel -8 {ov}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                },
            };
            if (!conv.Start())
                throw new System.Exception("Can not start ffmpeg.exe");
            return conv;
        }
    }
}
