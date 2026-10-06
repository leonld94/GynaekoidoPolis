# Unity project tooling

- This repository uses the Unity CLI. Project-local `unity-cli` and `unity-pipeline` skills are installed under `.agents/skills`; if the plugin-qualified name is shown, `unity:unity-cli` is the same CLI skill. For every Unity-related task, load the applicable skill before running Unity commands.
- The sandbox-safe CLI executable is installed at `.unity-cli/bin/unity.exe` relative to the repository root. In PowerShell, invoke it as `& (Join-Path $PWD '.unity-cli\bin\unity.exe')`.
- Do not conclude that Unity CLI is missing merely because bare `unity` is absent from the current process PATH. Use the project-local executable above as the canonical fallback.
- `com.unity.pipeline` is installed in `Packages/manifest.json`. Before changing scenes, GameObjects, prefabs, or Unity assets, run `status --format json`. If a connected Editor is ready, use its Pipeline commands instead of hand-editing Unity YAML.
- Pass `--project-path` when more than one Editor may be open. Pass `--caller plugin --skill unity-cli` on every `unity command` invocation produced directly by the Unity CLI skill.
- If `status` reports no instances while the Editor appears open, check `pipeline list` for Safe Mode and remember that sandbox isolation can hide a running Editor.
