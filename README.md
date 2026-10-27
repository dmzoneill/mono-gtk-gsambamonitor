# GSambaMonitor

<p align="center">
  <img src=".github/logo.png" alt="mono-gtk-gsambamonitor logo" width="128" />
</p>

A Mono GTK# window that monitors Samba activity.

## What it does

It runs `smbstatus` on a timer and shows the output in a text view, so you can
watch who is connected to the Samba shares and which files are open. Monitoring
can be started and stopped.

## Tech

- C# with Mono and GTK#
- Samba (`smbstatus`)

## Building

Open the `.sln` file in MonoDevelop, or build it with Mono and run the resulting
`.exe` with `mono`. Requires GTK# 2.

## License

Licensed under the Apache License 2.0. See [LICENSE](LICENSE).
