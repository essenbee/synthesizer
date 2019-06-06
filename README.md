# synthesizer
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors)

This is a project on my live coding stream [Codebase Alpha](https://twitch.tv/codebasealpha) starting with episode 34. The aim of the project is to explore some of the features of the extensive the **NAudio** digital audio library. To do this, I'm using **.NET Core 3.0** and **WPF** to develop a simple sythesizer, starting with a monophonic keyboard, but going on throughout the project to introduce such things as polyphony, ADSR envelopes, instruments and voices, and visualisations (such as a spectrum analyser). Basically, let's see how far we can take this!

Please note that, despite this being a .NET Core project, because I'm using **NAudio** and **WPF**, this code is Windows-only at this time.

The NAudio Github repo can be found [here](https://github.com/naudio/NAudio).

## Progress

#### Episode 34

A simple monophonic synthesizer was created. The code needs tidying up but its working at a basic level. Next up: add polyphony anf the ability to change the octave the keyboard covers!

#### Episode 35

Added a GUI octave selector and had initial stab at making the keyboard polyphonic.

## Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table><tr><td align="center"><a href="https://github.com/mrange"><img src="https://avatars2.githubusercontent.com/u/2491891?v=4" width="100px;" alt="mrange"/><br /><sub><b>mrange</b></sub></a><br /><a href="https://github.com/essenbee/synthesizer/commits?author=mrange" title="Code">💻</a> <a href="#ideas-mrange" title="Ideas, Planning, & Feedback">🤔</a></td></tr></table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
