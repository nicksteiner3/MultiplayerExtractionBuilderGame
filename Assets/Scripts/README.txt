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

Core Loop Hooks
- [ ] Power gating for machines via `PowerManager`
- [ ] Manual fueling system (bio-tier) for early power
- [ ] Self-sufficient generators (coal-like tier) milestone
- [ ] Multi-input recipes + throughput scaling
- [ ] Extraction cycle validation (enter/exit flow, drop-off)
- [ ] Death/losing equipped abilities rules
- [ ] Save/load persistence for ship state
- [ ] Triumphs tracking and UI

Seasonal & Endgame
- [ ] Milestones catalogue (tiers, materials, rewards)
- [ ] Extraction Nexus construction phases (1–5)
- [ ] Ranked Extraction mode scaffolding
- [ ] Expedition/retirement flow + rewards
- [ ] Ships management UX (3–5 ship slots)

Systems & Rooms
- [ ] Tether mechanic implementation
- [ ] Practice range room
- [ ] Power UI (graphs, alerts)
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
- [ ] Add tests/validation steps where practical
