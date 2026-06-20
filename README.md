# 🚲 Bicycle Delivery Rush

> **A 3D delivery-themed racing game built in Unity (URP) where you ride a bicycle through a bustling low-poly city, dodging AI-controlled traffic, obeying traffic signals, and making deliveries as fast as possible.**

![Unity](https://img.shields.io/badge/Unity-6000.x-000000?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-10.0-239120?logo=csharp&logoColor=white)
![URP](https://img.shields.io/badge/Render%20Pipeline-URP%2017.3-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-In%20Development-orange)

---

## 📖 Table of Contents

- [About the Project](#-about-the-project)
- [Key Features](#-key-features)
- [Screenshots & Gameplay](#-screenshots--gameplay)
- [Tech Stack](#-tech-stack)
- [Project Architecture](#-project-architecture)
- [Code Structure — Deep Dive](#-code-structure--deep-dive)
  - [BicycleMovement.cs](#bicyclemovementcs)
  - [CarAIMovement.cs](#caraimovementcs)
  - [CarAIMovement_Simple.cs](#caraimovement_simplecs)
  - [TrafficSignal.cs](#trafficsignalcs)
  - [TrafficManager.cs](#trafficmanagercs)
  - [RoadIntersectionCheck.cs](#roadintersectioncheckcs)
- [Input System](#-input-system)
- [Assets Used](#-assets-used)
- [Development Timeline](#-development-timeline)
- [Skills Learned](#-skills-learned)
- [Skills Currently Learning](#-skills-currently-learning)
- [Getting Started](#-getting-started)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [Acknowledgements](#-acknowledgements)

---

## 🎮 About the Project

**Bicycle Delivery Rush** is a 3D game being developed in **Unity** using the **Universal Render Pipeline (URP)**. The player controls a bicycle through a vibrant low-poly city environment, navigating through realistic AI-controlled traffic, traffic signals, and city intersections. The goal is to deliver packages across the city as quickly and safely as possible.

This project is a hands-on learning exercise in:
- Unity game development
- Vehicle physics simulation
- AI-driven traffic systems
- Game architecture & design patterns

---

## ✨ Key Features

| Feature | Description |
|---|---|
| 🚲 **Realistic Bicycle Physics** | WheelCollider-based physics with front steering, rear motor torque, speed clamping, and independent front/rear braking |
| 🚗 **AI Traffic System** | Spline-based car AI that follows paths, detects obstacles via BoxCast, and decelerates progressively |
| 🚦 **Traffic Signals** | Fully functional Green → Yellow → Red signal cycle that AI vehicles obey |
| 🏙️ **Low-Poly City** | Beautiful low-poly city environment with buildings, roads, intersections, nature, and props |
| 🎮 **New Input System** | WASD movement + Space braking using Unity's modern Input System with Input Actions |
| 🔧 **Traffic Manager** | Centralized manager that coordinates multiple AI vehicles on the same spline to prevent pile-ups |
| 🛣️ **Road Intersection Logic** | Trigger-based intersection checks that control vehicle right-of-way |
| 🎨 **URP Rendering** | Universal Render Pipeline with both PC and Mobile render pipeline asset configurations |

---

## 🛠️ Tech Stack

| Technology | Version / Details |
|---|---|
| **Engine** | Unity 6000.x (Unity 6) |
| **Language** | C# 10 |
| **Render Pipeline** | Universal Render Pipeline (URP) 17.3.0 |
| **Input** | Unity Input System 1.18.0 |
| **Splines** | Unity Splines (via URP package) |
| **Camera** | Cinemachine 3.1.6 |
| **Navigation** | AI Navigation 2.0.10 |
| **Timeline** | Unity Timeline 1.8.10 |
| **IDE** | Visual Studio / Rider |
| **Version Control** | Git + GitHub |

---

## 🏗️ Project Architecture

```
Bicycle-Delivery-Rush/
├── Assets/
│   ├── Character/                    # Player character model (FBX) and textures
│   │   ├── Ch01_nonPBR.fbx          # 3D character model
│   │   └── Character(T&M)/          # Diffuse, Normal, Specular, Glossiness maps
│   │
│   ├── InputActions/                 # Unity Input Action Assets
│   │   └── BikeInputs.inputactions   # Custom input map (Move + Brake)
│   │
│   ├── Materials/                    # Custom materials
│   │   ├── Bicycle_Material.mat      # Main bicycle body material
│   │   └── Wheels.mat               # Wheel material
│   │
│   ├── MotorcyclePack_/             # Motorcycle 3D model asset pack
│   │   ├── MotorcyclePack_SRP_Build_/
│   │   └── MotorcyclePack_URP_Build_/
│   │
│   ├── Prefabs/                      # Reusable game object prefabs
│   │   ├── Bike.prefab              # Base bicycle prefab
│   │   └── Main_Bike.prefab         # Main player bicycle (with all components)
│   │
│   ├── Scenes/                       # Unity scenes
│   │   ├── DeliveryRush.unity        # Main game scene (3.1 MB — full city)
│   │   ├── SampleScene.unity         # Default URP sample scene
│   │   └── TestingScene.unity        # Physics & AI testing sandbox
│   │
│   ├── Scripts/                      # All C# gameplay scripts
│   │   ├── BicycleMovement.cs        # Player bicycle controller
│   │   ├── CarAIMovement.cs          # WheelCollider-based car AI (advanced)
│   │   ├── CarAIMovement_Simple.cs   # Spline-following car AI (primary)
│   │   ├── TrafficSignal.cs          # Traffic light state machine
│   │   ├── TrafficManager.cs         # Multi-car coordination manager
│   │   └── RoadIntersectionCheck.cs  # Intersection trigger logic
│   │
│   ├── Settings/                     # URP render pipeline configs
│   │   ├── PC_RPAsset.asset          # PC quality render settings
│   │   ├── PC_Renderer.asset         # PC renderer config
│   │   ├── Mobile_RPAsset.asset      # Mobile quality render settings
│   │   ├── Mobile_Renderer.asset     # Mobile renderer config
│   │   └── DefaultVolumeProfile.asset # Post-processing volume
│   │
│   ├── SimplePoly City - Low Poly Assets/  # City asset pack
│   │   ├── Models/                   # FBX models (100+ assets)
│   │   │   ├── Buildings (26 types — shops, houses, stadium, factory…)
│   │   │   ├── Vehicles (10 types — car, taxi, bus, truck, ambulance…)
│   │   │   ├── Roads (lanes, corners, intersections, sidewalks…)
│   │   │   ├── Nature (trees, bushes, grass, rocks…)
│   │   │   └── Props (benches, signs, traffic signals, lights…)
│   │   ├── Prefab/                   # Pre-configured prefabs
│   │   ├── Materials/                # Asset materials
│   │   └── Textures/                 # Asset textures
│   │
│   └── TutorialInfo/                 # Unity template tutorial assets
│
├── Packages/
│   └── manifest.json                 # Unity package dependencies
│
├── ProjectSettings/                  # Unity project configuration
└── README.md                         # ← You are here
```

---

## 🔬 Code Structure — Deep Dive

### `BicycleMovement.cs`
> **📍 Location:** `Assets/Scripts/BicycleMovement.cs` · **81 lines**

The **player controller** — handles all bicycle physics and input processing.

**Architecture:**
```
BicycleMovement (MonoBehaviour)
├── Input Layer (Unity New Input System)
│   ├── PlayerInput component
│   ├── moveAction → Vector2 (WASD)
│   └── brakeButton → Button (Space)
│
├── Physics Layer (WheelColliders + Rigidbody)
│   ├── frontWheelCollider → Steering
│   ├── rearWheelCollider → Motor + Braking
│   └── Rigidbody → Physics simulation
│
└── Visual Layer (Mesh Sync)
    ├── frontWheelMesh → Synced via GetWorldPose()
    └── rearWheelMesh → Synced via GetWorldPose()
```

**Key Mechanics:**
- **Speed Clamping:** Uses `Vector3.Dot()` to calculate forward speed, then applies a `speedFactor = 1 - (currentSpeed / maxSpeed)` to smoothly reduce torque as the bike approaches max speed
- **Dynamic Turning:** Turn radius adapts to speed via `turnFactor = Lerp(1.3, 0.4, speed/maxSpeed)` — tight turns at low speed, stable at high speed
- **Dual Braking:** Independent front (`800 Nm`) and rear (`1000 Nm`) brake torque for realistic stopping
- **Wheel Mesh Sync:** `LateUpdate()` synchronizes visual wheel meshes with physics WheelCollider positions using `GetWorldPose()`

---

### `CarAIMovement.cs`
> **📍 Location:** `Assets/Scripts/CarAIMovement.cs` · **144 lines**

An **advanced WheelCollider-based car AI** that uses spline paths with physics-based steering and braking.

**Architecture:**
```
CarAIMovement (MonoBehaviour)
├── Path Following (SplineContainer)
│   ├── spline → Assigned path
│   ├── startOffset → Where on spline to begin
│   └── t → Current position parameter
│
├── Speed Control
│   ├── BoxCast detection (lookAheadDistance = 30)
│   ├── rightTurnLayer detection
│   └── Progressive braking via brakeFactor curve
│
└── Wheel Physics
    ├── rearWheelColliders[] → Motor torque
    ├── frontWheelColliders[] → Steering angle
    └── Speed-proportional torque/braking
```

**Key Mechanics:**
- **BoxCast Turn Detection:** Casts a box forward to detect upcoming turns via `rightTurnLayer`, then calculates braking distance as `max(currentSpeed * 1.5, 10)`
- **Smooth Deceleration Curve:** `brakeFactor = Clamp01(1 - hitDistance / brakingDistance)` creates a smooth braking curve that gets stronger as the car gets closer to the turn
- **Speed-Proportional Torque:** Motor torque scales with `1 - currentSpeed / maxSpeed` for natural acceleration feel

---

### `CarAIMovement_Simple.cs`
> **📍 Location:** `Assets/Scripts/CarAIMovement_Simple.cs` · **289 lines**

The **primary car AI system** — a comprehensive spline-following controller with obstacle detection, traffic signal obedience, intersection handling, and turn anticipation.

**Architecture:**
```
CarAIMovement_Simple (MonoBehaviour)
├── Movement System
│   ├── Spline-based position (MovePosition)
│   ├── Spline-tangent rotation (MoveRotation + Slerp)
│   ├── Acceleration/Deceleration rates
│   └── Speed clamping
│
├── Detection System (3 independent BoxCast layers)
│   ├── obstacleLayer → General obstacle avoidance
│   ├── trafficLayer → Traffic signal detection
│   └── roadIntersectionLayer → Intersection right-of-way
│
├── Turn Anticipation System
│   ├── Current tangent angle calculation
│   ├── Look-ahead tangent comparison
│   └── Dynamic BoxCast direction adjustment (±30°)
│
└── Public API (for TrafficManager)
    ├── GetTValue() / GetCurrentSpeed()
    ├── GetSplinePath()
    └── SetCanMove() / SetCanTurn()
```

**Key Mechanics:**
- **6-Tier Obstacle Response:** Progressive deceleration based on distance (`<3m` = full stop, `<5m` = heavy brake, `<8m` = medium, `<10m` = light, `<15m` = gentle, `<20m` = minimal)
- **Traffic Signal Integration:** Reads `TrafficSignal.SignalState` enum — full stop on Red close range, deceleration on Red far range, slow-down on Yellow, full speed on Green
- **Turn Anticipation:** Compares current spline tangent angle with look-ahead tangent to predict turns, then rotates the BoxCast direction by ±30° to "look around corners"
- **Spline Looping:** When `t >= 1`, resets to `startOffset` for continuous circuit driving

---

### `TrafficSignal.cs`
> **📍 Location:** `Assets/Scripts/TrafficSignal.cs` · **47 lines**

A **timer-based traffic signal state machine** with three states.

**State Cycle:**
```
┌──────────┐    10s     ┌──────────┐    3s     ┌──────────┐    7s
│  GREEN   │ ────────→  │  YELLOW  │ ────────→ │   RED    │ ────────→ (reset)
└──────────┘            └──────────┘           └──────────┘
   0-10s                  10-13s                  13-20s
```

**Key Details:**
- Uses a `SignalState` enum: `Green`, `Red`, `Yellow`
- Total cycle time: **20 seconds** (10s green, 3s yellow, 7s red)
- Publicly exposed `currentSignal` so AI vehicles can read the current state

---

### `TrafficManager.cs`
> **📍 Location:** `Assets/Scripts/TrafficManager.cs` · **36 lines**

A **centralized traffic coordinator** that prevents AI vehicle pile-ups on shared spline paths.

**Logic Flow:**
```
For each consecutive car pair (i, i+1):
  ├── Same spline path?
  │   ├── Car[i] behind Car[i+1]?  (tValue comparison)
  │   │   └── Car[i+1] nearly stopped? (speed < 3)
  │   │       └── YES → Stop Car[i] (canMove=false, emergency brake)
  │   └── NO → Continue normally
  └── Different paths → Skip
```

---

### `RoadIntersectionCheck.cs`
> **📍 Location:** `Assets/Scripts/RoadIntersectionCheck.cs` · **23 lines**

A **trigger-based intersection controller** using Unity's collision system.

- `OnTriggerEnter` / `OnTriggerStay` → Sets `canMove = false` (blocks vehicles)
- `OnTriggerExit` → Sets `canMove = true` (clears intersection)
- Used by `CarAIMovement_Simple.TestRoadIntersection()` to implement right-of-way logic

---

## 🎮 Input System

The game uses **Unity's New Input System** with custom Input Action Assets:

| Action | Type | Binding | Description |
|---|---|---|---|
| **Move** | Value (Vector2) | `W/A/S/D` | Forward/backward acceleration + left/right steering |
| **Brake** | Button | `Space` | Apply front & rear brakes simultaneously |

Input Action Asset: `Assets/InputActions/BikeInputs.inputactions`

---

## 🎨 Assets Used

| Asset | Type | Description |
|---|---|---|
| **SimplePoly City** | 3D Models | Low-poly city pack with 100+ FBX models — buildings, vehicles, roads, nature, and props |
| **Motorcycle Pack** | 3D Models | Motorcycle models (URP & SRP compatible) used as the bicycle base |
| **Character Model** | FBX + Textures | Non-PBR humanoid character with diffuse, normal, specular, and glossiness maps |

---

## 📅 Development Timeline

| Date | Milestone | Commit |
|---|---|---|
| **May 7, 2026** | 🚀 Initial project setup — Unity URP template, asset imports | `0f650b1` |
| **May 7, 2026** | 🖼️ Updated URP configuration image | `905452a` |
| **May 17, 2026** | 🚲 Implemented bicycle physics — WheelCollider steering, torque, braking, wheel mesh sync | `b832e76` |
| **Jun 16, 2026** | 🐛 Bug fixes — refined car AI behavior and physics tuning | `83e736a` |
| **Jun 19, 2026** | 🚗 Added TrafficManager, improved car AI look-ahead turn detection | `08518ed` |

---

## 🧠 Skills Learned

### Unity & Game Development
- ✅ **Unity Editor Workflow** — Scene management, Inspector configuration, prefab creation, and project organization
- ✅ **Universal Render Pipeline (URP)** — Setting up URP, configuring Renderer Assets for PC and Mobile, Volume Profiles, and post-processing
- ✅ **Unity Physics (3D)** — Rigidbody dynamics, WheelColliders, motor torque, brake torque, steer angles, and `GetWorldPose()` for mesh synchronization
- ✅ **New Input System** — Input Action Assets, `PlayerInput` component, `ReadValue<Vector2>()`, `IsPressed()`, action maps, and composite bindings (2D Vector)

### Programming Concepts
- ✅ **Vehicle Physics Simulation** — Implementing realistic bicycle mechanics with WheelColliders, speed-dependent turn radius, dual braking systems, and speed clamping
- ✅ **AI Pathfinding with Splines** — Using `SplineContainer` for path definition, `EvaluatePosition()` and `EvaluateTangent()` for movement, and `Rigidbody.MovePosition()` / `MoveRotation()` for physics-based AI movement
- ✅ **State Machines** — Implementing a traffic signal state machine with enum states and timer-based transitions
- ✅ **Raycasting & BoxCasting** — Using `Physics.BoxCast()` for obstacle detection with multiple layers, distance-based response tiers, and debug visualization
- ✅ **Progressive Deceleration Algorithms** — Designing multi-tier braking responses based on obstacle distance with smooth interpolation
- ✅ **Layer-Based Collision Detection** — Using `LayerMask` to separate obstacle, traffic, and intersection detection into independent systems
- ✅ **Quaternion Math** — `Quaternion.LookRotation()`, `Quaternion.Slerp()`, `Quaternion.Euler()` for smooth rotations and turn predictions

### Software Engineering
- ✅ **Component Architecture** — Separating concerns into focused MonoBehaviours (movement, signals, management, intersection checks)
- ✅ **Public API Design** — Getter/setter methods on `CarAIMovement_Simple` to allow `TrafficManager` to coordinate vehicles without tight coupling
- ✅ **Prefab Workflow** — Creating reusable prefabs (`Bike.prefab`, `Main_Bike.prefab`) for consistent game object configuration
- ✅ **Git Version Control** — Meaningful commit messages, `.gitignore` configuration for Unity projects, GitHub repository management
- ✅ **Multi-Platform Configuration** — Setting up separate URP Renderer Assets for PC and Mobile targets

---

## 📚 Skills Currently Learning

- 🔄 **Advanced AI Traffic Coordination** — Improving the `TrafficManager` to handle more complex multi-lane scenarios and intersection priority
- 🔄 **Road Intersection Right-of-Way** — Implementing trigger-based intersection logic (currently commented out in `CarAIMovement_Simple`, under active development)
- 🔄 **Turn Anticipation Systems** — Refining the spline look-ahead algorithm that predicts upcoming turns and adjusts BoxCast direction dynamically
- 🔄 **Delivery Game Mechanics** — Planning pickup/drop-off locations, timers, scoring, and route optimization
- 🔄 **UI/UX for Games** — HUD elements, mini-maps, delivery indicators, speedometers
- 🔄 **Cinemachine Camera System** — Implementing dynamic camera follow behavior using Cinemachine 3.x
- 🔄 **Character Animation** — Integrating character animations with the bicycle for pedaling, leaning, and idle states
- 🔄 **Scene Design & Level Layout** — Building a cohesive city environment with the SimplePoly City asset pack

---

## 🚀 Getting Started

### Prerequisites
- **Unity 6000.x** (Unity 6) or later
- **Git** for version control

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Vedant241/Bicycle-Delivery-Rush.git
   ```

2. **Open in Unity Hub:**
   - Open Unity Hub → Click **"Open"** → Navigate to the cloned directory
   - Unity will auto-install required packages from `manifest.json`

3. **Open the main scene:**
   - Navigate to `Assets/Scenes/DeliveryRush.unity`
   - Press **Play** ▶️

### Controls

| Key | Action |
|---|---|
| `W` | Accelerate forward |
| `S` | Reverse |
| `A` | Steer left |
| `D` | Steer right |
| `Space` | Brake |

---

## 🗺️ Roadmap

- [ ] Delivery pickup/drop-off system with waypoints
- [ ] Timer-based scoring mechanism
- [ ] HUD — speedometer, mini-map, delivery status
- [ ] Cinemachine third-person camera follow
- [ ] Character pedaling animations
- [ ] Sound effects & background music
- [ ] Mobile touch controls
- [ ] Multiple city levels / routes
- [ ] Pedestrian AI
- [ ] Day/night cycle

---

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 🙏 Acknowledgements

- [**SimplePoly City**](https://assetstore.unity.com/) — Low-poly city asset pack for buildings, roads, vehicles, and props
- [**Motorcycle Pack**](https://assetstore.unity.com/) — 3D motorcycle models (URP compatible)
- [**Unity Technologies**](https://unity.com/) — Game engine, URP, Input System, Splines, Cinemachine
- [**Mixamo**](https://www.mixamo.com/) — Character model and textures

---

<p align="center">
  Made with ❤️ and Unity
</p>
