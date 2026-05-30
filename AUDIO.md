# Audio Manager Documentation

## Architecture

The audio system is handled by `AudioSceneFading.cs` (located at `Assets/scripts/AudioSceneFading.cs`). It is a singleton MonoBehaviour attached to a GameObject in the scene.

It uses **two arrays** to hold all audio clips:
- `musicClips` (`AudioClip[]`) — background music tracks
- `sfxClips` (`AudioClip[]`) — sound effects

**There is no hardcoded limit.** Both arrays scale to any size through the Unity Inspector. The system already supports 30+ music tracks and 20+ sound effects with zero code changes.

---

## Current Audio Files

| Index | Type | File | Location | Used In |
|-------|------|------|----------|---------|
| 0 | Music | `BGM_Title.ogg` | `Assets/music/` | Unity scene (looping) |
| 1 | Music | `BGM_New_High_Score.ogg` | `Assets/music/` | Not called yet |
| 0 | SFX | `SND_Menu_Click.ogg` | `Assets/sound_effects/` | Continue screen tap/click |
| 1 | SFX | `SND_Title_Say.ogg` | `Assets/sound_effects/` | Title screen |

---

## How to Add New Music or Sound Effects

### Step 1: Import Audio Files
Place your `.ogg`, `.wav`, or `.mp3` files in:
- Music: `Assets/music/`
- Sound effects: `Assets/sound_effects/`

### Step 2: Assign in Unity Inspector
1. Open your scene in Unity
2. Find the GameObject that has the `AudioSceneFading` script attached
3. In the Inspector, locate the **Music Clips** and **SFX Clips** arrays
4. Increase the **Size** field to add new slots
5. Drag your AudioClip assets into the new slots
6. **Remember the index** of each clip — that's the number you pass when playing

### Step 3: Play from Code
```csharp
// Play a music track (looping)
AudioSceneFading.PlayMusic(index, -1);

// Play a music track (once, no loop)
AudioSceneFading.PlayMusic(index, 0);

// Play a sound effect (one-shot, no loop)
AudioSceneFading.PlaySoundEffect(index, 0);

// Play a sound effect (looping — note: only one looping SFX at a time)
AudioSceneFading.PlaySoundEffect(index, -1);
```

### Step 4: Adjust Volume (Runtime)
```csharp
AudioSceneFading.MusicVolume = 0.8f;    // 0.0 to 1.0
AudioSceneFading.EffectsVolume = 0.6f;  // 0.0 to 1.0
```

---

## API Reference

### Static Methods

| Method | Parameters | Description |
|--------|-----------|-------------|
| `PlayMusic(int index, int loop)` | `index` — array index of the music clip<br>`loop` — `-1` for infinite loop, anything else for one-shot | Plays a music track on the dedicated music AudioSource |
| `PlaySoundEffect(int index, int loop)` | `index` — array index of the SFX clip<br>`loop` — `-1` for looping, anything else for one-shot (PlayOneShot) | Plays a sound effect. Non-looping SFX use `PlayOneShot` so multiple SFX can overlap |

### Static Properties

| Property | Description |
|----------|-------------|
| `MusicVolume` | Global music volume (0.0 to 1.0, default 0.5) |
| `EffectsVolume` | Global SFX volume (0.0 to 1.0, default 0.5) |
| `MusicCurrentlyPlaying` | Read-only: index of the currently playing music track, or -1 if none |

---

## Notes

- **SFX overlap:** One-shot sound effects stack naturally via Unity's `PlayOneShot`. Multiple SFX can play simultaneously.
- **Looping SFX limitation:** Only one looping sound effect can play at a time (uses the single SFX AudioSource). This is rarely needed — most SFX are one-shots.
- **Memory:** 30+ music tracks loaded into the `musicClips` array will all reside in memory. Consider using `Resources.Load` or Addressables if memory becomes an issue at scale.
