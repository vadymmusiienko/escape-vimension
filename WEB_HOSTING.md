# Hosting "Escape the VimEnsion" on the web

The game is built for **WebGL** (runs in any modern browser). The build output is a
folder of static files — `index.html` + a few subfolders — so it can be hosted on any
static host.

## What the build produces

```
Build/WebGL/
├── index.html
├── Build/            # the actual game (.data / .wasm / .framework.js / .loader.js)
├── TemplateData/     # styling + Unity logo
└── StreamingAssets/  # (only if the game uses streaming assets)
```

The build is made with **compression disabled** (`WebGLBuilder.cs`), so it works on hosts
that can't set custom HTTP headers (notably GitHub Pages). No server configuration needed.

## Test it locally first

WebGL builds must be served over HTTP — opening `index.html` from `file://` will not work.

```bash
cd Build/WebGL
python3 -m http.server 8080
# then open http://localhost:8080
```

## Deploy options

### Option A — GitHub Pages (free, repo already on GitHub)

> Note: `.gitignore` ignores the `Build/` folder by default, so you must either
> copy the output into `docs/` (recommended) or force-add it (`git add -f Build/WebGL`).

1. Copy the build into a `docs/` folder: `cp -r Build/WebGL/* docs/` and commit `docs/`.
2. In the repo: **Settings → Pages → Build from a branch**, pick the branch and `/docs`.
3. Your game will be live at `https://<user>.github.io/<repo>/`.

GitHub Pages can't set `Content-Encoding`, which is exactly why the build uses _disabled_
compression — no extra config required.

### Option B — Netlify / Vercel (free, drag-and-drop or git)

- **Netlify Drop:** go to https://app.netlify.com/drop and drag the `Build/WebGL` folder in.
- **Vercel/Netlify via git:** set the output/publish directory to `Build/WebGL`, no build
  command needed (the files are pre-built).
- These hosts _can_ set headers, so you can later switch to **Gzip or Brotli** compression
  in `WebGLBuilder.cs` for smaller downloads (≈3–4× smaller). With Gzip + matching
  `Content-Encoding` headers the game loads faster.

### Option C — itch.io (game-focused)

1. Zip the **contents** of `Build/WebGL` (so `index.html` is at the zip root).
2. Create a new project on itch.io, set kind to **HTML**, upload the zip, tick
   "This file will be played in the browser."

## Rebuilding after code/asset changes

```bash
/Applications/Unity/Hub/Editor/6000.5.0f1/Unity.app/Contents/MacOS/Unity \
  -quit -batchmode -projectPath . -buildTarget WebGL \
  -executeMethod WebGLBuilder.Build -logFile build.log
```
