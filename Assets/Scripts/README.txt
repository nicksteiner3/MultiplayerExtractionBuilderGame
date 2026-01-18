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
- [ ] Display "Press E" interaction prompt when looking at interactable objects
- [ ] Add Reactor to ship building system (buildable machine alongside Fabricator)
- [ ] Implement machine disassembly (return salvage cost when destroying placed machines)
- [ ] Create pause menu (settings, resume, quit, controls, audio, graphics)
- [ ] Create title screen (new game, continue, settings, quit)

Core Power & Machines
- [x] Power gating for machines via `PowerManager`
- [x] Manual fueling system (reactor) for early power
- [x] Power UI displays capacity and consumption
- [x] Fabricator consumes power while crafting
- [ ] Equipment terminal powered (gating ability equip)
- [ ] Ship building terminal powered (gating machine placement)

Material Types & Recipes
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
