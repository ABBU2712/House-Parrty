# House Party VR - Preliminary Prototype
**Course**: CS 6334 Virtual Reality  
**Team Name**: Psych creations (Team 05)  
**Platform**: Google Cardboard / Android Phones   
**Demo Date**: April 16 
**GitHub URL**: https://github.com/ABBU2712/House-Parrty

---

## 🎮 Overview

**House Party VR** is a virtual reality experience set during a high-energy party inside a digital house. The prototype focuses on player interaction with an AI-driven Non-Playable Character (NPC) who can dance, converse, and react dynamically to the player through contextual UI and animations.

---

## 📌 Unity Scene

The main prototype is located in the Unity scene:


---

## 🕹️ Interaction Techniques

### 🧑 Player Inputs
| Input | Action |
|-------|--------|
| `X` (keyboard or controller) | Initiates conversation with NPC |
| `Y` (keyboard or controller) | Triggers NPC dance and party-related response |
| Gaze/Pointer | Aims at NPC to enable contextual options |
| `I` | Shows interaction menu |

### 🧠 Interaction Summary
- NPC reacts to player key input via raycast-based targeting.
- NPC responses trigger floating chat bubbles with contextual animations (talk/dance).
- The interaction menu appears when the NPC is idle and the player is focused.

---

## 🎛️ Controller Support

- Supports **Fortune Wireless Controller**
- Controller mappings are based on Unity’s legacy input system:
  - `X` → JoystickButton0
  - `Y` → JoystickButton3
- Menu options dynamically display assigned keys:  
  Example: `"Talk (X)"`, `"Dance (Y)"`

---

## 🎨 Features Implemented

✅ Basic Requirements:
- VR scene with environment and interactive NPC  
- Raycast interaction system  
- UI menu with button responses  
- Basic character animation: talking & dancing  
- Dialogue system with mood, memory, and response management

✅ Advanced Features:
- Dynamic ChatBubble UI with text + background  
- Emotion-based dialogue categories ("hello", "dance")  
- NPC remembers player actions (via NPCMemory system)  
- Dynamic Player Response UI injected into the existing menu system  

---

## 📱 Target Device

- Secondary support for **Android phones** via Cardboard-style input

---

## 📦 Required Assets & SDKs

See [`Source.md`](Source.md) for complete links and licensing info.

---

## 📽️ Demo Video

🎬 YouTube Link: 
---

## 👨‍👩‍👧 Team Info

- Member names: Abinash Mishra / Amitesh Singh Bais
- Assigned roles: Presenter / Presenter
- Contribution breakdown: Abinash contributed in the initial setup followed by designing and devloping NPCs from scratch.
                          Amitesh designed the complete scene along with making the beer pong game work.

---

## 🧪 How to Run

1. Clone the GitHub repository:
    ```bash
    git clone https://github.com/ABBU2712/House-Parrty
    ```

2. Open the project in Unity **2021.x or later**.

3. Plug in Oculus Quest 2 or Android device.

4. Build the scene to APK:
    - File → Build Settings → Android → Build → Name: `preliminary.apk`

5. Deploy to device for full VR experience.

---

## ⚠️ Notes

- Scene optimized for demo day performance (under 800MB).
- Player menu is shown only after each NPC response, based on the current interaction context.
- Please ensure `.gitignore` is set correctly to avoid pushing temporary files.

---


