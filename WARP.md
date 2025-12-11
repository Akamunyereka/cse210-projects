# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Repository Overview

This is a C# learning repository for CSE 210 (Programming with Classes). The repository contains multiple independent console application projects organized by week, each targeting .NET 8.0. Projects are starter templates intended for educational exercises.

## Project Structure

```
cse210-projects/
├── sandbox/Sandbox/          # Empty sandbox project for experimentation
├── week01/Exercise[1-5]/     # Week 1 exercises
├── week02/Journal, Resumes/  # Week 2 projects
├── week03/Fractions, ScriptureMemorizer/
├── week04/YouTubeVideos, OnlineOrdering/
├── week05/Homework, Mindfulness/
├── week06/Shapes, EternalQuest/
├── week07/ExerciseTracking/
└── cse210-ww-student-template.sln
```

Each project folder contains:
- `ProjectName.csproj` - .NET 8.0 console application project file
- `Program.cs` - Main entry point with starter code

## Common Commands

### Building Projects

Build a specific project:
```powershell
dotnet build week02\Journal\Journal.csproj
```

Build the entire solution:
```powershell
dotnet build cse210-ww-student-template.sln
```

### Running Projects

Run a specific project:
```powershell
dotnet run --project week02\Journal\Journal.csproj
```

Or navigate to the project directory and run:
```powershell
cd week02\Journal
dotnet run
```

### Working with the Sandbox

The sandbox project is for experimentation:
```powershell
dotnet run --project sandbox\Sandbox\Sandbox.csproj
```

### VS Code Integration

The repository includes pre-configured VS Code tasks and launch configurations. Use F5 to debug any project through the VS Code debugger, or run tasks via the Command Palette (Ctrl+Shift+P > "Tasks: Run Task").

Available build tasks follow the pattern: `build-weekXX-ProjectName` (e.g., `build-week02-Journal`)

## Project Configuration

All projects share the same configuration:
- **Target Framework**: .NET 8.0
- **Output Type**: Console application (Exe)
- **Implicit Usings**: Enabled
- **Nullable Reference Types**: Disabled

## Development Practices

When working on projects in this repository:

1. **Project Independence**: Each week's projects are independent. Changes to one project should not affect others.

2. **Simple Console Applications**: All projects are console applications with straightforward entry points in `Program.cs`. Most starter code is minimal (basic "Hello World" style).

3. **File Organization**: Keep all code within the respective project directory. Don't create cross-project dependencies.

4. **Target Framework**: Always maintain .NET 8.0 as the target framework. Don't upgrade or downgrade without intentional reason.

5. **Working Directory Context**: When running commands, be aware of whether you're at the repository root or within a specific project folder. Commands may need adjusted paths accordingly.
