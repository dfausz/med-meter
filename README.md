# 💊 MedMeter  
### _Track Your Medications, Offline and Effortlessly_

**MedMeter** is a minimalist, offline-first medication tracker built with **Xamarin**, designed for speed, reliability, and simplicity.  
It helps users record doses, manage safe intervals, and visualize upcoming availability using a sleek animated countdown wheel.

![app screenshot](https://github.com/dfausz/MedMeter/blob/main/medmeter.png?raw=true)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
![app screenshot](https://github.com/dfausz/MedMeter/blob/main/update.png?raw=true)

---

## 🧰 Tech Stack
| Layer | Tools / Frameworks |
|--------|---------------------|
| Framework | [Xamarin.Forms](https://dotnet.microsoft.com/apps/xamarin) |
| Language | C# (.NET) |
| Local Storage | [SQLite](https://www.sqlite.org/index.html) |
| Graphics | [SkiaSharp](https://github.com/mono/SkiaSharp) |
| Architecture | MVVM (Model-View-ViewModel) |
| Testing | Unit + Integration Tests |

---

## ✨ Features
- 📱 **Built in Xamarin (C#)** – Cross-platform mobile app supporting Android & iOS  
- 💾 **Offline SQLite Storage** – All data is stored locally; no internet required  
- 🎡 **SkiaSharp Animated Wheel** – Visual countdown until your next allowed dose  
- 📸 **MediaPicker Integration** – Capture a photo of each medication for quick identification  
- 🧩 **MVVM Architecture** – Clean separation of concerns with testable services and viewmodels  
- 🧪 **Unit & Integration Tests** – Core logic verified for reliability and accuracy  

---

## 🚀 Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/dfausz/med-meter.git
cd med-meter
```

### 2. Open in Visual Studio
- Use **Visual Studio 2022** (Windows or Mac) with **.NET SDK + Xamarin workload** installed.  
- Open `MedMeter.sln`.

### 3. Build & Run
- Select target platform (Android / iOS Emulator).  
- Run the project using the ▶️ **Start Debugging** button.

---

## 🧩 Project Structure
```
MedMeter/
├── MedMeter/
│   ├── MedMeter.Android/             
│   ├── MedMeter.UWP/             
│   ├── MedMeter.iOS/             
|   └── MedMeter/
│       ├── ViewModels/         # MVVM logic and command bindings
│       ├── Views/              # XAML UI pages
│       ├── Models/             # Data model objects
│       ├── Services/           # SQLite data service, timer service, photo capture
│       ├── Controls/           # SkiaSharp drawing logic for the animated wheel
│       ├── Converters/         # Converters used in XAML for view logic
│       ├── Resources/          # Images and fonts
│       ├── Utilities/          # Files with logic for reuse around the app
│       ├── App.xaml.cs         # App lifecycle and navigation
│       └── MedMeter.csproj     # Main project configuration
│
├── IntegrationTests/       # Integration tests
├── UnitTests/              # Unit tests
├── MedMeter.sln            # Solution file
└── README.md
```

---

## 💡 Design Goals
MedMeter’s goal is to **reduce medication anxiety** through simplicity and clarity:  
- One-tap logging for fast dose entry  
- Animated feedback for time tracking  
- No clutter — minimal screens, maximal clarity  
- 100% offline operation for reliability anywhere  

---

## 🧑‍💻 Author
**Daniel Fausz**  
Senior Software Engineer (Front-End / .NET MAUI / Xamarin / React)  
[fausz.dev](https://fausz.dev) · [GitHub](https://github.com/dfausz)

---

Icons made by [Freepik](https://www.freepik.com) from [https://www.flaticon.com/](www.flaticon.com)
