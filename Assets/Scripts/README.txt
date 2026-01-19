# Multiplayer Extraction Builder Game

## Overview
- Core: PvPvE extraction meets ship-building and ability crafting.
- Loop: Run extractions → bring back salvage → build machines → craft abilities → return stronger.
- Multiplayer: Ships are player-owned; choose solo ship or team ships.

## Seasonal Structure
- Length: 3 months per season; aim for 10+ seasons.
- Cycle: Start limited → unlock systems via milestones → retire via expeditions for permanent account-wide rewards.
- Reset: Ship progress resets each season; account carries select unlocks.

## Core Gameplay Loop
- Extract: Venture into PvPvE zones as often as you want; risk vs reward.
- Build: Use salvage to construct ship machines and terminals.
- Craft: Manufacture abilities, weapons, upgrades using recipes.
- Optimize: Improve efficiency and throughput to hit bigger milestones.
- Compete: Shift to Ranked Extraction once endgame goals are met.

## Full Gameplay Loop
- First Session: Choose a starter ship, learn manual power and basic crafting; equip your first ability with limited battery constraints.
- Prep Phase (each session): Set objectives, equip abilities, check power/fuel, queue manufacturing tasks on the ship.
- Deploy Phase: Enter PvPvE zone, gather salvage, engage AI/players, manage risk and extraction timing.
- Return & Unload: Dock and manually unload salvage into ship stash; failure in-zone means loss of carried items.
- Build & Craft: Construct machines and terminals, unlock recipes, manufacture abilities/weapons/upgrades; manage equip slots and stash.
- Power & Unlocks: Progress from manual fueling to self-sufficient generators via milestones; machines operate when powered.
- Milestones Progression: Complete tiered objectives requiring both PvPvE materials and manufactured parts to unlock systems and efficiency.
- Nexus Construction: Advance the 5-phase Extraction Nexus as the season’s tangible monument and victory condition.
- Ranked Extraction: After Nexus completion, compete in ranked events using optimized ship builds and team compositions.
- Expedition & Reset: Retire to gain permanent rewards (cosmetics, extra ship slot, +1 ability slot, blueprint persistence, 10% salvage conversion) and begin a new season with strategic advantages.
- Season Cadence: Early (0–50h), Mid (50–150h), End (150–200h) phases guide pacing toward endgame competition.

## Power Model
- Early: Finite battery + manual fueling; machines run when powered, not hard-locked if unlocked.
- Progression: Unlock self-sufficient power through milestone tiers (bio → coal-like → advanced generators).
- Late: Practically unbounded power with enough inputs; soft cap emerges when generation exceeds demand.
- Ownership: Power is unique per ship; players can maintain 3–5 ships (solo, friend-group, general).

## Milestones
- Structure: Satisfactory-style tiers; each milestone requires specific materials.
- Sources: Mix of PvPvE-gathered materials and ship-manufactured components.
- Rewards: Unlock machines, abilities, weapons, character upgrades, power tiers, and efficiency boosts.
- Difficulty: Milestones scale in complexity and throughput requirements.

## Ultimate Seasonal Goal: Extraction Nexus
- Tangible Monument: A multi-phase, visible centerpiece built on the ship.
- Phases (1–5): Power Core, Stabilizer, Extraction Array, Control Lattice, Singularity Anchor.
- Requirements: Each phase demands materials from PvPvE and manufactured parts.
- Completion: Fully operational Nexus unlocks Ranked Extraction Mode and serves as season victory condition.

## Ranked Extraction Mode
- Access: Unlocked by completing the Extraction Nexus.
- Play: Competitive, team-based extraction events leveraging your ship loadout and abilities.
- Meta: Endgame focus after maximizing systems; leaderboards and seasonal prestige.

## Expeditions (Retirement) and Permanent Rewards
- Trigger: After significant progress (e.g., Nexus completion or milestone threshold), retire the character/ship.
- Rewards:
	- Cosmetics/Prestige Badge: Visual veteran status.
	- Extra Ship Slot: +1 ship slot for next season to diversify builds.
	- Ability Slot +1: Start next season with an additional equipped ability slot.
	- Blueprint Persistence: Fully researched machine blueprints persist account-wide.
	- Salvage Conversion: 10% of final season salvage converts to startup salvage next season.
- Balance: Meaningful head start without invalidating the fresh-start seasonal experience.

## Early/Mid/Endgame
- Early (0–50h): Learn extraction, basic building, manual power; craft first abilities; small battery constraints.
- Mid (50–150h): Unlock self-sufficient power, expand machine network, craft advanced abilities/weapons; push milestone tiers.
- End (150–200h): Maximize systems, complete Nexus, enter Ranked Extraction; optimize builds and team comps.

## Triumphs (Account Checklist)
- Structure: Repeatable achievements, e.g., “Complete 50 extractions,” “Craft 200 abilities,” “Generate X sustained power,” “Win Y ranked events.”
- Purpose: Long-term goals across seasons; unlock titles, cosmetics, and minor account buffs.

## Ships and Ownership
- Slots: Maintain 3–5 ships; assign roles (solo, specific friend group, general-use).
- Isolation: Each ship’s power and build are distinct; progression choices matter.

## Design Pillars
- Player Agency: Frequent extractions deepen ship progression.
- Visible Progress: Tangible monument (Nexus) shows advancement.
- Seasonal Freshness: Meaningful resets with prestige and permanent rewards.
- Skillful PvPvE: Risk management and competitive mastery at endgame.

## Material Economy & Progression Framework

### Resource Tiers
**Tier 0: Raw (PvPvE Only, At-Risk)**
- Salvage Scrap: Common, everywhere, basic currency
- Ore Fragments: Mineral deposits, for refining
- Bio-Matter: Organic drops, fuel source
- Circuit Wreckage: Rare tech nodes, electronics

**Tier 1: Refined (Ship-Only, Safe)**
- Processed Alloy: Ore + power → Refinery
- Fuel Cells: Bio-Matter + power → Fuel Processor
- Logic Chips: Circuit Wreckage + power → Electronics Bench

**Tier 2: Components (Ship, Some Portable)**
- Ability Modules: Logic Chips + Alloy → Fabricator
- Weapon Frames: Alloy + Salvage → Assembly Line (can take to PvP)
- Power Cores: Fuel Cells + Logic Chips → Advanced Fabricator

**Tier 3: Products (Equipped or Consumed)**
- Finished Abilities: Modules + Cores + Salvage
- Weapons: Frames + Logic Chips (lootable corpses)
- Consumables: Fuel Cells + Bio-Matter (high-risk PvP targets)

### Risk Model
- **At-Risk Materials:** Tier 0 raw mats, carried by players, lootable on death
- **Why Risk Them?** Early game manual gathering; later, bait/trade; consumables for PvP edge
- **Safe Materials:** Tier 1 & 2 on ship; too valuable to carry
- **Ability Loss:** Dying means losing equipped abilities (cost of failure, PvP stakes)

### Automation Timeline
- **Hour 0-10:** Manual everything
- **Hour 10-30:** Refinery unlocked (process ore)
- **Hour 30-50:** Self-sufficient reactor (no manual refuel)
- **Hour 50-80:** Harvester drones (passive gathering)
- **Hour 80-120:** Conveyor belts (auto-routing)
- **Hour 120-150:** Assembly lines (10x throughput)
- **Hour 150-180:** Full automation (focus shifts to PvP)
- **Hour 180-200:** Nexus complete → Ranked Extraction



## Prototype Checklist

Gameplay Basics
- [x] FPS movement + camera
- [x] Interaction via `IInteractable` and `PlayerInteraction`
- [x] Salvage pickup and manual unload to ship stash
- [x] Fabricator machine + dynamic recipe UI (runtime loading)
- [x] Ability crafting: `DashAbility` from recipe
- [x] Equipment UI: equip ability and use in-game
- [x] Ship building terminal + UI + placement system
- [x] Salvage consumption on build
- [x] Inactive UI finding fix for terminals
- [x] Camera raycast origin fix (use player camera)
- [x] Reactor machine + dynamic placement
- [x] Reactor start/fueling mechanics
- [x] Multi-material recipe system (materialInputs/outputs)
- [x] SessionState material tracking (Dictionary<MaterialData, int>)
- [x] Tutorial system (0-10min: Place Reactor → Start Reactor → Place Fabricator → Craft Dash → Equip Dash)
- [x] Tutorial extension (Deploy → Return detection → Challenge prompt → Milestone prompt)
- [ ] Display "Press E" interaction prompt when looking at interactable objects
- [ ] Implement machine disassembly (return salvage cost when destroying placed machines)
- [ ] Create pause menu (settings, resume, quit, controls, audio, graphics)
- [ ] Create title screen (new game, continue, settings, quit)
- [ ] **BUG: Equipment terminal/Stash errors upon interacting after returning from deploy**
- [ ] Challenge system (track progress, notify TutorialManager on completion)
- [ ] Milestone system (aggregate challenge progress, notify on completion)

Core Power & Machines
- [x] Power gating for machines via `PowerManager`
- [x] Manual fueling system (reactor) for early power
- [x] Power UI displays capacity and consumption
- [x] Fabricator consumes power while crafting
- [x] Reactor buildable via ship building system
- [x] Reactor start/on-off mechanics
- [ ] Equipment terminal powered (gating ability equip)
- [ ] Ship building terminal powered (gating machine placement)

Material Types & Recipes

## Material System Overview

## Testing

### Running Two Fabricators At Once
- Setup: Place two Fabricator machines and ensure power capacity ≥ combined draw (each uses 10 power).
- Seed: Use SessionState dev starts to add enough Bio-Fuel and Salvage Scrap for two crafts.
- Start: Open each Fabricator terminal and start the same Dash recipe on both.
- Expect: Both progress bars advance concurrently; power UI shows doubled consumption.
- Verify: Each completion spawns an ability item into stash; if stash fills, machines stop and release power.
- Notes: Auto-repeat is enabled; ensure sufficient inputs or it will pause on missing materials.

## Legacy Cleanup
- [ ] Remove legacy salvage integer system (fields `runSalvage`, `stashSalvage`) and related UI/readouts.
- [ ] Remove legacy `ItemCost` salvage path in recipes; migrate all recipes to `materialInputs` only.
- [ ] Delete legacy fallback in `FabricatorMachine` (`HasInputs()`/`ConsumeInputs()` salvage branches).
- [ ] Remove unload flow (`UnloadInventory()` and salvage pickups) or convert to material-based pickups/unload.
- [ ] Verify Equipment/Inventory UI no longer references salvage; replace with material counts where needed.

## First 10 Minutes: Guided Tutorial

### Tutorial Flow
Linear sequence guiding the player through the core loop:
1. **Place Reactor** - Build a Reactor from the ship building terminal to enable power generation
2. **Start Reactor** - Activate the Reactor to begin power production
3. **Place Fabricator** - Build a Fabricator to enable ability crafting
4. **Craft Dash** - Use the Fabricator to craft the Dash ability (uses Bio-Matter + Salvage)
5. **Equip Dash** - Equip Dash at the Equipment Terminal
6. **Deploy** - Launch your first extraction from the Launch Terminal
7. **Return from Deploy** - Auto-detected on scene reload; objective updates
8. **Complete First Challenge** - Tutorial advances to challenge system
9. **Complete First Milestone** - Tutorial advances to milestone system
10. **Complete** - Tutorial done; player now self-guided

### Tutorial Implementation
- `TutorialManager`: Singleton tracking progression, persists with `DontDestroyOnLoad`
- Hooks: Events from `ShipBuilder`, `Reactor`, `FabricatorMachine`, `PlayerAbilities`, `LaunchTerminal`
- UI: `Player Prompt Panel` displays current objective text; generic for all prompts (goals, challenges, etc.)
- Persistence: Progress saved to `SessionState.tutorialStep` (int)
- Return Detection: Listens to `SceneManager.sceneLoaded` to detect deploy returns

### Onboarding Goals
- **Starter Resources**: SessionState provides starting Bio-Fuel and Salvage Scrap so first 10 minutes flows smoothly
- **Objective Text**: Updates as player progresses; shows next step clearly
- **Toast Notifications**: Console logs (TODO: implement UI toast popup)

## Challenges & Milestones System

### Challenge System (TODO)
- Simple tasks that grant progress toward milestones
- Example: "Craft 2 abilities", "Gather 100 Bio-Matter", "Complete 1 extraction"
- Progress tracked per session
- Notify `TutorialManager.OnChallengeCompleted()` when first challenge is done

### Milestone System (TODO)
- Aggregate checkpoint requiring multiple challenges or specific conditions
- Example: "Complete 3 challenges" or "Craft 5 abilities total"
- Unlocks new machines or ability tiers
- Notify `TutorialManager.OnMilestoneCompleted()` when first milestone is done

### Challenges & Milestones TODOs
- [ ] Create `ChallengeData` ScriptableObject (id, title, description, condition, reward)
- [ ] Create `ChallengeManager` to track active challenges, listen for completion events
- [ ] Create `MilestoneData` ScriptableObject (id, title, required challenges, reward)
- [ ] Create `MilestoneManager` to track progress, aggregate challenge status
- [ ] Wire challenge progress (craft ability → increment counter, notify manager)
- [ ] Wire milestone progress (all challenges complete → notify manager)
- [ ] Create simple UI for active challenges (list, progress bars)
- [ ] Create simple UI for milestone tracking (progress toward requirement)

## Known Issues & Critical TODOs

### Critical Bugs
- [ ] **Equipment Terminal/Stash Errors After Deploy**: Interacting with Equipment Terminal or Stash immediately after returning from a deploy causes errors. Likely due to SessionState or UI state corruption on scene reload. Needs investigation and fix.

### UI Polish (Post-MVP)
- [ ] Toast UI popup (currently console-only)
- [ ] Highlight/ping system for tutorial objectives (outline Reactor, glow Fabricator terminal, etc.)
- [ ] "Press E" interaction prompt overlay
- [ ] Auto-save/checkpoint system

### Tier 1: Raw Materials (8 types from PvPvE zones)
- Salvage Scrap (default drop, all zones)
- Ore Fragments (metal-based, Tier 0 resource)
- Bio-Matter (organic-based, Tier 0 resource)
- Circuit Wreckage (electronics, rare Tier 0 resource)
- Crystal Shards (energy-based, Tier 0 resource)
- Synthetic Fibers (lightweight, Tier 0 resource)
- Rare Earth Minerals (catalysts, rare Tier 0 resource)
- Composite Slag (heavy materials, Tier 0 resource)

### Tier 2: Processed Intermediates (10+ types - FUTURE)
- Processed Alloy (from Ore via Refinery)
- Fuel Cells (from Bio-Matter via Fuel Processor)
- Logic Chips (from Circuits via Electronics Bench)
- Crystal Matrix (from Crystals via Crystal Lab)
- Synthetic Composite (from Fibers via Fiber Mill)
- Catalyst Core (from Rare Earths via Catalyst Extractor)
- Energy Capacitor (from Crystals variant via Energy Lab)
- Organic Polymer (from Bio-Matter variant via Bio-Processor)
- Reinforced Slag (from Slag via Furnace)
- Concentrated Essence (from Volatile via Essence Refiner)

### Current Machine Focus (Phase 1)
- **Fabricator** - Crafts abilities and weapons from **raw materials + salvage**
- NO processing machines in Phase 1 (keep loop simple)
- All recipes use direct raw materials (Ore, Bio-Matter, Circuits, etc.)

### Phase 2: Material Processing Machines (10-50h+)

**Refiner Machine**
- Input: Ore Fragments (3)
- Output: Processed Alloy (1)
- Craft Time: 10s
- Power Cost: 10
- Purpose: Convert cheap raw ore into concentrated metal for advanced recipes

**Bio-Processor Machine**
- Input: Bio-Matter (3)
- Output: Fuel Cells (1)
- Craft Time: 10s
- Power Cost: 10
- Purpose: Convert organic material into energy-dense fuel for advanced recipes

**Design Philosophy:**
- Raw materials are plentiful but heavy (3 ore → 1 alloy)
- Processing requires time + power investment
- Processed materials unlock higher-tier recipes on Advanced Machine
- Players choose specialization: "I'll focus on ore refining" or "bio processing"

### Phase 3+: Additional Processors (50h+)
- Electronics Bench - Circuits → Logic Chips (3:1, 15s)
- Crystal Lab - Crystals → Crystal Matrix (2:1, 12s)
- Fiber Mill - Fibers → Synthetic Composite (3:1, 10s)
- Catalyst Extractor - Rare Earths → Catalyst Core (4:1, 20s)
- And 3-4 more as content scales

### Advanced Machine (Phase 2+, unlock at 10h)
- Handles 2+ input recipes using processed materials
- Example: Processed Alloy (1) + Fuel Cells (2) + Salvage (50) → Jetpack Tier 1
- Requires players to invest in processing chain
- 20+ recipes enabling deep specialization

### Recipe System Updates
- **RecipeData.cs** updated to support multiple inputs/outputs
- Inputs now use `List<MaterialInput>` (material type + quantity)
- Outputs now use `List<MaterialOutput>` (material type + quantity)
- Fabricator handles multi-input recipes (checks all materials before crafting)
- Machine-level power consumption (not per-recipe variant)
- Recipes specify `requiredMachine` (Fabricator or Advanced Machine)

### Phase 1 Recipe Examples (Fabricator Only - Raw Materials Only)
Simple 1-2 input recipes using raw materials (no processing):

**Dash Ability Tier 0**
- Input: Bio-Matter (1) + Salvage (30)
- Output: Dash Ability
- Craft Time: 20s
- Power Cost: 10
- Unlock: Milestone 1

**Heal Orb Tier 0**
- Input: Bio-Matter (1) + Salvage (25)
- Output: Heal Orb Ability
- Craft Time: 15s
- Power Cost: 10
- Unlock: Milestone 1

**Pistol Tier 0** - Requires 1 Ore + Salvage
- Input: Ore Fragments (1) + Salvage (40)
- Output: Pistol Weapon
- Craft Time: 25s
- Power Cost: 10
- Unlock: Milestone 1

### Phase 1 Recipe TODOs
- [x] Create Dash recipe (Bio-Matter + Salvage)
- [ ] Create Heal Orb recipe (Bio-Matter + Salvage)
- [ ] Create Pistol recipe (Ore + Salvage)
- [ ] Create Rifle recipe (Ore + Salvage variant)
- [ ] Create 1-2 more basic recipes to test loop
- [x] Test material consumption and output
- [x] Test Fabricator with new material system

### Phase 2: Advanced Machine System (10h+ unlock)
- Create "Assembly Machine" or "Advanced Fabricator" (TBD name)
- Supports 2+ input recipes
- Designed for processed materials (Fuel Cells, Logic Chips, etc.)
- 20+ recipes for complex items
- Unlocked at Milestone 5
- Output: Dash Recipe
- Craft Time: 20s
- Power Cost: 10

**Rifle Weapon (Tier 0)** - Requires Metal + Logic
- Input: Processed Alloy (2) + Logic Chips (1) + Salvage (40)
- Output: Rifle Recipe
- Craft Time: 25s
- Power Cost: 10

**Heal Orb (Tier 0)** - Requires Bio + Crystal
- Input: Organic Polymer (1) + Crystal Matrix (1) + Salvage (25)
- Output: Heal Orb Recipe
- Craft Time: 15s
- Power Cost: 10

**Phase 1: Material System TODOs**
- [x] Create MaterialData.cs ScriptableObject (name, tier, icon, description)
- [x] Create 8 raw material assets in Resources/Materials/
- [x] Update SessionState.cs to track all materials (Dictionary<MaterialData, int>)
- [x] Add material display methods (GetMaterial, AddMaterial, RemoveMaterial, HasMaterial)
- [x] Create MaterialStack struct (material type + quantity, for inventory display)
- [x] Test material tracking in SessionState (debug logs for adds/removes)

**Phase 2: Machine System TODOs**
- [ ] Create MachineData.cs ScriptableObject (name, power cost, input material, output material, process time)
- [ ] Create 8 machine assets in Resources/Machines/
- [ ] Create generic MachineProcessor class (handles material conversion)
- [ ] Implement machine crafting loop (input consumption → timer → output creation)
- [ ] Add machine status tracking (running, paused, broken)
- [ ] Test machine on-demand crafting (convert materials when activated)

**Phase 3: Recipe System TODOs**
- [x] Create MaterialInput struct (MaterialData + quantity)
- [x] Create MaterialOutput struct (MaterialData + quantity)
- [x] Update RecipeData.cs to support List<MaterialInput> and List<MaterialOutput>
- [x] Create multi-input recipe validation (check all materials exist before crafting)
- [x] Update FabricatorMachine.cs to handle multiple inputs (ConsumeInputs loops through list)
- [x] Create Dash ability recipe with material costs
- [ ] Create 4 more ability recipes (Heal Orb, Shield, Radar, etc.) with material costs
- [ ] Create 5 weapon recipes (Pistol, Rifle, Shotgun, Sniper, Arc Gun) with material costs
- [ ] Test full production chain (extract → process → craft → equip)
- [ ] Balance recipe costs (ensure progression pacing, not too expensive/cheap)

**Current TODOs (from previous sections):**
- [ ] Define Salvage Scrap as tier 0 base resource
- [ ] Add Ore Fragments resource (PvPvE drop)
- [ ] Add Bio-Matter resource (PvPvE drop)
- [ ] Add Circuit Wreckage resource (rare PvPvE)
- [ ] Create Refinery machine + recipe (Ore → Processed Alloy)
- [ ] Create Fuel Processor machine + recipe (Bio-Matter → Fuel Cells)
- [ ] Create Electronics Bench machine + recipe (Circuit → Logic Chips)
- [ ] Update Fabricator recipes to use Tier 1 materials
- [ ] Design 5 ability recipes with Tier 0-2 costs
- [ ] Design 5 weapon recipes with Tier 0-2 costs

Weapons & Abilities Framework
## Core 5 Abilities (Playstyle Anchors)
- Dash: Offensive mobility, quick repositioning
- Radar Pulse: Information gathering, enemy detection (counter: stealth)
- Shielding: Defensive tanking, damage absorption (counter: high DPS)
- Cloak/Fade: Evasion and stealth (counter: detection/radar)
- Overcharge: Power/speed boost, enhanced stats (counter: healing/outlasting)

## Core 5 Weapons (Engagement Archetypes)
- Pistol: Reliable, quick-swap, early game default
- Rifle: Medium-range balanced, versatile
- Shotgun: Close-range burst, high damage (counter: distance/mobility)
- Sniper: One-shot power, slow fire rate (counter: movement/cover)
- Utility (Arc Gun/Mines/EMP): Crowd control, area denial, setup

## Progression Tier System
- Tier 0: Starter equipment (pistol, dash, basic shield) - free at start
- Tier 1: Early farm items (3-4 hours progression) - basic variants of core items
- Tier 2: Mid-game items (zone B viable) - improved stats, new variants unlock
- Tier 3: Advanced items (20+ hours grind) - specialized for specific playstyles
- Tier 4: Legendary end-game items (100+ hour commitment) - extremely overpowered, droppable on battlefield, require repair/durability management

## Items & Variants Philosophy
- Use variant/upgrade system, not completely unique mechanics per item
- Every item has counters (rock-paper-scissors across loadouts)
- Early-game items can be upgraded through progression
- End-game items are expensive, powerful, and eventually wear out (require resources to repair)
- Long-term: 50 weapons, 100 abilities, 30 utility items, 10+ shield levels (variant-based)

## TODOs for Weapons & Abilities
- [ ] Define base weapon and ability properties (damage, cooldown, cost, durability)
- [ ] Create AbilityData and WeaponData ScriptableObjects
- [ ] Implement Tier 0 variants for each core ability
- [ ] Implement Tier 0 variants for each core weapon
- [ ] Design Tier 1 variants (stat improvements, new effects)
- [ ] Create ability cooldown and energy system
- [ ] Create weapon ammo/durability tracking
- [ ] Implement ability equip/unequip UI in Equipment Terminal
- [ ] Implement weapon equip/unequip UI in Equipment Terminal
- [ ] Add weapon firing mechanics (raycast hit detection, damage application)
- [ ] Add ability activation mechanics (input handling, visual/audio feedback)
- [ ] Design challenge system to force players to use less-familiar loadouts
- [ ] Balance all items against each other (no one-meta problem)

## Potential Abilities Library (Brainstorm & Design Concepts)
These are candidate abilities for future implementation. Each will be evaluated for viability, balance, and counter-play mechanics.

**Mobility/Movement:**
- Jetpack (sustained flight, resource drain)
- Grapple (swing to distant points, repositioning tool)
- High Jump (single powerful vertical boost)
- Double Jump (two consecutive jumps)
- Triple Jump (three consecutive jumps)
- Long Jump (extended horizontal dash)
- Hold Jump to Hover (sustained float, controllable descent)
- Glide (sustained diagonal descent with control)
- Charge & Spear (dash that drags enemy with you, direction-dependent impact)
- Wall Ride (run along vertical surfaces)
- Hold onto Walls (cling to vertical surfaces, reposition)
- Shift into Invincible (travel in any direction, increased speed, brief invulnerability window)

**Offensive/Combat:**
- Beam that Auto-Locks (limited range, tracking projectile)
- Disk that Bounces (multi-target hitscan, ricochet mechanic)
- Ground Slam (requires height to activate, AoE damage/stun on impact)
- Grab and Slam (melee stun and damage combo)
- Charge & Spear (see Mobility section—dual-purpose)
- Stun Orb (thrown short-distance projectile, applies stun)
- Fast-Moving Projectile Stun (rapid projectile with crowd control)
- Flaming Wall (vertical line of fire that travels forward)
- Force Push/Pull (telekinetic manipulation of objects/enemies)
- Deployable Turrets (stationary offensive support)
- Controllable Mech Suit (temporary vehicle with enhanced damage/durability)
- Deflect (reflect incoming projectiles back at enemies)
- Shoot Projectiles on Ground (create damaging lines between connected shots)

**Defensive/Protection:**
- Self Resurrection (revive after death with cooldown, limited charges)
- Big Shield (manually deployable, absorbs damage until broken, indefinite hold time)
- Shield Wall (stationary shield between two points: origin and activation location)
- Make You and Nearby Allies Immune (brief window, area-based protection)
- Become Invincible but Unable to Damage (tactical repositioning without dealing damage)
- Field that Nullifies Projectiles (all projectiles pulled in and neutralized)
- Flaming Wall (see Offensive—also protective barrier)

**Support/Team Play:**
- Orb Healing Over Time (attaches to teammate, continuous healing)
- Drop Health Orbs (placeable healing items for self and team)
- Damage Boost (buff self and nearby teammates, stat amplification)
- Apply Damage Boost/Nerf to Target (selective buff/debuff on any player)
- Damage Boost to You and Teammates (area-of-effect enhancement)

**Utility/Control:**
- Mark Target (reveals enemy position, enables team coordination)
- Slow on Hit (movement speed reduction, multiple implementations possible)
- Take Damage to Store Energy (damage-as-currency mechanic, risk-reward activation)
- Throw Up Walls Telepathically (create temporary environmental obstacles)
- Create Clones Where You Aim (decoys or actual clones, confusion/distraction)
- Lifesteal (heal from damage dealt, sustain mechanic)

**Experimental/Complex:**
- Controllable Mech Suit (temporary vehicle mode, high-skill ceiling)
- Create Clones (high skill expression, positioning dependent)
- Shift into Invincible (high-speed evasion, skill-based timing)

**Balance Considerations for Future Design:**
- High mobility abilities need counter-play (detection, slow, area denial)
- Offensive/crowd control needs cooldown management and positioning risk
- Support abilities scale with team size (multiplayer relevance)
- Defensive abilities need to have clear breaking points (duration, damage threshold)
- Damage conversion abilities (damage → energy, slow on hit) add complexity but reward skill

## Milestone & Recipe Unlock System

**Core Philosophy:**
- Milestones unlock **recipes**, not items
- Progression: Unlock recipe → Gather resources → Build machines → Craft item → Equip → Use
- Recipes are the reward; crafting is the investment
- Rarity comes from resource scarcity and factory complexity, not arbitrary gatekeeping

**Progression Timeline:**
- **Milestone 1-5 (0-50h):** Directed unlocks, core abilities + machines, building foundation
- **Milestone 6-15 (50-150h):** Hybrid unlocks + Ability Shards, player choice within guidance
- **Milestone 16+ (150-200h+):** Specialty unlocks, deep specialization paths

**Ability Shards Currency System:**
- Awarded per milestone completion (varies by difficulty)
- Tier 0 recipes cost 1 Shard
- Tier 1 recipes cost 2 Shards
- Tier 2+ recipes cost 3+ Shards
- Players spend shards on abilities they want to unlock (not dictated)
- Encourages multiple playthroughs with different unlock choices each season

## Milestone 1: "Boot Camp" (0-10 hours)

**Challenges:**
- [ ] Craft 5 Salvage Scraps (basic crafting tutorial)
- [ ] Deal 100 damage with Pistol (weapon familiarization)
- [ ] Survive 10 minutes in PvPvE zone (zone survival)
- [ ] Collect 200 Ore Fragments (resource gathering)
- [ ] Equip and use Dash ability 5 times (ability usage)

**Unlocks:**
- Dash LV2 recipe (upgraded version)
- Rifle recipe
- Heal Orb recipe (support ability)
- Refinery machine recipe
- 5x Ability Shards (for choosing 5 Tier 0 abilities to unlock)

**Implementation TODOs:**
- [ ] Create Milestone tracking system (track challenge progress)
- [ ] Create challenge definition system (scriptable object for challenges)
- [ ] Implement challenge completion detection (damage dealt, items crafted, time survived)
- [ ] Create shard currency system and UI display
- [ ] Create recipe unlock system (track unlocked recipes per player)
- [ ] Create ability unlock UI (show available shards, available recipes, confirm unlock)
- [ ] Design 20 milestone definitions (Milestone 1-20 with unique challenges)
- [ ] Design shard reward distribution (how many shards per milestone)
- [ ] Create milestone completion rewards UI (celebration/notification)
- [ ] Implement seasonal milestone reset (reset on character retirement/new season)
- [ ] Design unlock progression table (which recipes unlock at which milestones)
- [ ] Create challenge variety (20+ unique challenge types to prevent repetition)

Fabricator UI Enhancements

- [x] Show craft time on each recipe button
- [x] Show salvage cost on each recipe button
- [x] Show recipe description/tooltip on hover
- [x] Display crafting progress bar when active
- [x] Display current progress time (e.g., "15 / 20 seconds")
- [x] Change power model: machine has single power requirement (not per-recipe)
- [x] Hide recipe list after selection, show only current recipe + progress
- [x] Auto-queue next craft of same recipe when one completes
- [x] Stop crafting when stash is full
- [x] Add cancel button to stop crafting
- [ ] Show power availability status in recipe tooltip (e.g., "Insufficient Power" if machine can't run)
- [ ] Add status indicator showing why production is paused (insufficient power, stash full, insufficient materials)

Ship Building UI Enhancements
- [ ] Add exit button to ship building terminal UI (close without placing machine)

Output Routing System (Future Enhancement)
- [ ] Design container system: fabricator output → container inventory
- [ ] Implement IContainer interface for output receivers
- [ ] Create generic Container prefab (holds items, can be picked up)
- [ ] Create StashContainer variant (direct stash integration, unlimited capacity)
- [ ] Add output routing UI to fabricator (select destination: container or stash)
- [ ] Implement item transfer from container to player inventory
- [ ] Add multiple container support (chain containers together)
- [ ] Implement conveyor belt container routing (connect to containers)

Inventory System
- [ ] Create InventoryManager (tracks all items, slots, categories)
- [ ] Open inventory with TAB key (InventoryUI panel)
- [ ] Display resources (Salvage, Ore, Bio-Matter, Circuits)
- [ ] Display abilities (equippable, show cooldowns)
- [ ] Display weapons (equippable, show ammo/durability)
- [ ] Display consumables (healing, shields, buffs)
- [ ] Equip/unequip abilities to ability slots
- [ ] Equip/unequip weapons to weapon slots
- [ ] Prevent equipping non-equippable items (materials, consumables)
- [ ] Show inventory capacity per category (e.g., "12 / 20 Salvage")
- [ ] Drop item on ground (removes from inventory, creates world object)
- [ ] Pickup item from ground (adds to inventory or drops if full)
- [ ] Close inventory with ESC or TAB again
- [ ] Freeze player when inventory open (like other UIs)

Early Game Loop (0-50h)
- [ ] Unload salvage from PvPvE → goes to stash
- [ ] Manually craft 1st ability (test loop validation)
- [ ] Equip ability and use in PvP zone
- [ ] Return and unload, craft more
- [ ] Milestone 1: Craft 5 Dash abilities
- [ ] Milestone 2: Process 50 Ore into Processed Alloy

Mid Game Automation (50-150h)
- [ ] Self-sufficient reactor (burns fuel cells, no manual refuel)
- [ ] Harvester drone concept (place in zone, auto-gathers Salvage/Ore)
- [ ] Harvester drone implementation (prefab, placement, gathering loop)
- [ ] Harvester drone UI (show active drones, collection rate)
- [ ] Conveyor belt system (visual, routing logic)
- [ ] Conveyor belt UI (draw routes between machines)
- [ ] Milestone 3: Produce 100 Ability Modules
- [ ] Milestone 4: Build 10 Harvester Drones

Late Game Scaling (150-200h)
- [ ] Assembly line machine (parallel fabrication, 10x throughput)
- [ ] Auto-sorter (routes materials by type automatically)
- [ ] Multi-fabricator production chains (recipes spanning rooms)
- [ ] Nexus Phase 3: Deliver 500 Ability Modules + 200 Power Cores
- [ ] Nexus Phase 5: 1000 finished abilities produced
- [ ] Milestone tracking UI (show progress toward goals)

PvP Risk & Reward
- [ ] Carry consumables into PvPvE zone (healing, shields, buffs)
- [ ] Carry weapon frames for field repairs
- [ ] Implement consumable use (right-click healing item, applies effect)
- [ ] Consumables lost on death (add to drop table)
- [ ] Weapon frames droppable on death (lootable)
- [ ] Ability loss on death (player loses equipped ability, can retrieve?)

Extraction Cycle
- [ ] Extract from PvPvE zone (trigger return to ship)
- [ ] Inventory drop-off validation (can't craft if items not unloaded)
- [ ] Auto-unload inventory on docking (or manual button)
- [ ] Failed extraction penalty (die in zone, lose all carried items)
- [ ] Successful extraction bonus (unload goal amount → milestone progress)

Power Generation Tiers
- [ ] Tier 0: Manual reactor (refuel from stash, 25 power output)
- [ ] Tier 1: Self-sufficient reactor (burns fuel cells, auto-produce)
- [ ] Tier 2: Advanced reactor (higher output, lower consumption)
- [ ] Multiple reactors on ship (stackable power sources)
- [ ] Power grid visualization (show how much each machine consumes)

Persistence & Save/Load
- [ ] Save ship state (built machines, room layout)
- [ ] Save stash inventory (materials, quantities)
- [ ] Save equipped abilities per player
- [ ] Load ship state on session start
- [ ] Test load/save cycle (build, save, reload, verify)

Seasonal & Endgame
- [ ] Milestones catalogue (tiers, materials, rewards)
- [ ] Nexus phase descriptions (1-5, what they unlock)
- [ ] Ranked Extraction mode scaffolding
- [ ] Expedition/retirement flow + rewards
- [ ] Ships management UX (3–5 ship slots)

Progression Unlocks
- [ ] Unlock: +5 Salvage inventory capacity (Milestone 2)
- [ ] Unlock: +5 Ore inventory capacity (Milestone 3)
- [ ] Unlock: +5 Bio-Matter inventory capacity (Milestone 4)
- [ ] Unlock: +5 Circuit inventory capacity (Milestone 5)
- [ ] Unlock: +1 Ability equip slot (Milestone 7)
- [ ] Unlock: +1 Weapon equip slot (Milestone 8)
- [ ] Unlock: +5 Consumable stack size (Milestone 10)
- [ ] Unlock: Harvester drone deployment (Milestone 12)
- [ ] Unlock: Conveyor belt building (Milestone 15)
- [ ] Unlock: Assembly line crafting (Milestone 18)
- [ ] Create unlock tracking system (which unlocks player has)
- [ ] Display unlocked items in fabricator (hide locked recipes)
- [ ] Show unlock progress toward next milestone

Systems & Rooms
- [ ] Tether mechanic implementation
- [ ] Practice range room
- [ ] Resource types & economy balancing
- [ ] Multiplayer (deferred until core systems mature)

Content: Abilities & Weapons
- [ ] Design 5 abilities (names, roles, power costs)
- [ ] Implement 5 abilities (scripts, effects, cooldowns)
- [ ] Design 5 weapons (roles, ammo/power interactions)
- [ ] Implement 5 weapons (scripts, damage models, VFX/SFX)
- [ ] Create recipes for all 10 items (abilities + weapons)
- [ ] Define acquisition/unlock paths (drops, milestones, research) to enable crafting

Developer Status
- [x] Core loop validated: build machines, craft ability, equip, use
- [x] Power gating implemented and validated
- [x] Reactor system tested with crafting
- [ ] **Game Title:** Come up with permanent name (temp: "Extraction Protocol")
- [ ] Material economy framework ready for implementation
- [ ] Add tests/validation steps where practical
