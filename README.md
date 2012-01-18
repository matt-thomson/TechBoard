TechBoard
=========

TechBoard is an app designed for the needs of audio-visual technicians at live events.  It's designed to provide quick access to a range of simple tasks, and to be easily customizable.  I initially wrote TechBoard to play sound effects during a live comedy show, but I'm gradually extending it to cover a range of other purposes.

At its heart, TechBoard is a container for a bunch of mini-apps, known as blocks.  At present, there are a handful of blocks available, with more on their way.  TechBoard includes an easy-to-use editor for combining blocks into a board.  You can save and load boards, and transfer them between machines.

TechBoard is written using C#, and uses the Windows Presentation Foundation (WPF) framework.

Blocks
-------

Currently, the following block types are supported in TechBoard:

* Sound buttons
* Sound control buttons (play/fade)
* PowerPoint slideshow control buttons (next/previous slide, jump to numbered slide)
* Clock
* Stopwatch
* Label
* Divider

The following block types are available for developers only:

* GUID generator

Extending TechBoard
-------------------

It's easy to add new block types to TechBoard.  The blocks are based on WPF UserControls, and converting a UserControl to a block is a simple process of adding a few attributes.

At present, there is no written documentation of the plugin API.  If you're interested in writing your own plugins, please get in touch!