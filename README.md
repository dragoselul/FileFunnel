# FileFunnel

**Organize your files, eliminate duplicates, and streamline your storage.**

---

## Table of Contents

1. [Motivation](#motivation)
2. [Features](#features)
3. [Tech Stack](#tech-stack)
4. [Installation](#installation)
5. [Usage](#usage) - WIP
   - [CLI](#cli)
   - [UI](#ui)
6. [Roadmap](#roadmap)
7. [License](#license)

---

## Motivation

### <b>Status</b>: Early development—currently in MVP stage.

I was drowning in a sea of duplicates and a messy file system on my external hard drive—forgetting I already had copies of files, scattering work across random folders. FileFunnel solves this by watching a central "dump" folder, detecting duplicates, and sorting everything into a user-defined folder structure. Now you can find what you need, when you need it, without clutter and confusion.

---

## Features

- **Funnel Folder Watcher**: Automatically monitors a designated folder for new files and sorts them according to the rules set by the user.
- **Custom Rules-Based Sorting**: Define folder structures and matchers (extensions, regex) to route files where you want.
- **Duplicate Detection**: Identify and handle duplicate files to free up storage space.
- **Filesystem Organizer**: Moves and renames files to keep your drives neat and accessible.
- **CLI & GUI**: Full-featured command-line interface for scripting and a sleek Avalonia-based desktop UI.
- **Future 🎯**: Git-like versioning for documents (coming soon).

---

## Tech Stack

- **Language**: C#/.NET
- **UI Framework**: Avalonia (cross-platform desktop on Windows & Linux)
- **Watcher & Sorting Engine**: `.NET FileSystemWatcher` + regex/metadata-based rules

---

## Installation

Choose your platform and download:

- **Windows**: Portable `.exe` or installer (TBD).
- **Linux**: AppImage or distribution package (TBD).

```bash
# Clone the repo
git clone https://github.com/dragoselul/FileFunnel.git
cd FileFunnel

# Restore & build
dotnet restore
dotnet build -c Release

# RUN GUI
dotnet run --project FileFunnel
```
### UI

1. **Select Funnel Folder**: Browse to your central drop zone.  
2. **Define Rules**: Use the tree editor to add folders, extensions, and patterns.  
3. **Start Sorting**: Click "Start" to kick off the watcher service and let FileFunnel work its magic.

---

## Roadmap

- 🛠️ Core: Complete MVP of funnel folder watcher and sorting engine. - I am here right now 
- 🔍 Duplicate Finder: Enhanced hash-based dedupe matching.  
- 🗃️ Versioning: Git-like history for documents and rollback.  

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

> *Built with Avalonia and ❤️ by Dragos.*
