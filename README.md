# synthesizer
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors)
[![Build status](https://ci.appveyor.com/api/projects/status/3wbcayq7poqp3k6o/branch/master?svg=true)](https://ci.appveyor.com/project/essenbee/synthesizer/branch/master)
![Open Issues](https://img.shields.io/github/issues/essenbee/synthesizer.svg)
![PRs](https://img.shields.io/github/issues-pr-closed/essenbee/synthesizer.svg)
![Last Commit](https://img.shields.io/github/last-commit/essenbee/synthesizer.svg)

This is a project on my live coding stream [Codebase Alpha](https://twitch.tv/codebasealpha) starting with episode 34. The aim of the project is to explore some of the features of the extensive the **NAudio** digital audio library. To do this, I'm using **.NET Core 3.0** and **WPF** to develop a simple synthesizer, starting with a monophonic keyboard, but going on throughout the project to introduce such things as polyphony, ADSR envelopes, instruments and voices, and visualisations (such as a spectrum analyser). Basically, let's see how far we can take this!

Please note that, despite this being a .NET Core project, because I'm using **NAudio** and **WPF**, this code is Windows-only at this time.

The NAudio Github repo can be found [here](https://github.com/naudio/NAudio).

![Twitter](https://img.shields.io/twitter/follow/codebasealpha.svg?style=social)

## Progress

#### Episode 34

A simple monophonic synthesizer was created. The code needs tidying up but its working at a basic level. Next up: add polyphony anf the ability to change the octave the keyboard covers!

#### Episode 35

Added a GUI octave selector and had initial stab at making the keyboard polyphonic. Merged a PR that added a T4 template to generate the view model.

#### Episode 36

Merged a PR that added a spectrum analyser and waveform visualizer to the GUI. Moved from wavetables to signal generators in the `SynthWaveProvider` class, and implemented ADSR envelopes to shape the sound profile of notes. Finally, created a low pass filter and a tremolo effect for the synthesizer. The GUI elements of these latter were left for another stream.

## Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table><tr><td align="center"><a href="https://github.com/mrange"><img src="https://avatars2.githubusercontent.com/u/2491891?v=4" width="100px;" alt="mrange"/><br /><sub><b>mrange</b></sub></a><br /><a href="https://github.com/essenbee/synthesizer/commits?author=mrange" title="Code">💻</a> <a href="#ideas-mrange" title="Ideas, Planning, & Feedback">🤔</a></td></tr></table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
