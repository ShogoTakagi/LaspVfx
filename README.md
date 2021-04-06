LaspVfx (with Video Export)
-------
### The purpose of this fork
Experimental support audio export as video file via [uNvEncoder].

Spectrogram videos are useful for playing effects with music files and it works even if LASP unsupported environment. (i.e. Mobile)

### How to convert video files
At first, make sure the export has finished. (it needs much times after application quit(stop editor))

I recommend ffmpeg for convert H.264 to mov(mp4).

It needs change scene or close unity when you convert a file and permission dinied.

### music
https://dova-s.jp/bgm/play6955.html

[uNvEncoder]: https://github.com/hecomi/uNvEncoder

### About LaspVFX
![gif](https://i.imgur.com/KIwkpcI.gif)
![gif](https://i.imgur.com/Nrb1XGw.gif)

**LaspVfx** is a Unity example that shows how to use [LASP] to create audio
reactive effects with [Visual Effect Graph].

[LASP]: https://github.com/keijiro/Lasp
[Visual Effect Graph]: https://unity.com/visual-effect-graph

This project runs on Unity 2019.3.
