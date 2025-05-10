# House Party VR - Preliminary Prototype
**Course**: CS 6334 Virtual Reality  
**Team Name**: Psych creations (Team 05)  
**Platform**: Google Cardboard / Android Phones   
**Demo Date**: April 16 
**GitHub URL**: https://github.com/ABBU2712/House-Parrty
**Scene containing final prototype**: House

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
| `A` (keyboard or controller) | Settings Menu pop up|
| `X` (keyboard or controller) | Select an option in settings menu once it has popped up |
| `Y` (keyboard or controller) | Triggers NPC dance and party-related response |
| Gaze/Pointer | Aims at NPC to enable contextual options |
| `A` | Shows interaction menu |
| Bowling game |
| `Y` | Hold this to generate power for ball/ release to shoot |
| Use headset direction to control direction of raycast coming out of ball | Ball shoots in direction pointed while releasing Y

### 🧠 Interaction Summary
- NPC reacts to player key input via raycast-based targeting.
- NPC responses trigger floating chat bubbles with contextual animations (talk/dance).
- The interaction menu appears when the NPC is idle and the player is focused.

---

## 🎛️ Controller Support

- Supports **Fortune Wireless Controller**
- Menu options dynamically display assigned keys:  
  Example: `"Talk (X)"`, `"Dance (Y)"`

---

## 🎨 Features Implemented

✅ Basic Requirements:
- VR scene with environment and interactive NPC  
- Reticle pointer interaction system  
- UI menu with button responses  
- Basic character animation: talking & dancing  
- Dialogue system with mood, memory, and response management
- Settings menu with cool feature including change lights and sound of your choice
- An immersive bowling game with a score board and power bar display
- "Reflex rush": press up/down direction
- Very cool party like environment with static object including games, pizza, wine, TV, banners, sound and music

✅ Advanced Features:
- Dynamic ChatBubble UI with text + background  
- Emotion-based dialogue categories ("hello", "dance")  
- NPC remembers player actions (via NPCMemory system)  
- Dynamic Player Response UI injected into the existing menu system

 💻 Why we chose NPC AI:
We selected these requirements to ensure our NPC AI can engage in natural, context-aware conversations that adapt to player interactions.       This enhances immersion by making characters feel responsive, dynamic, and emotionally consistent.

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


