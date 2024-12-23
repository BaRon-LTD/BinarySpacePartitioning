# Room and Tile Map Generator with BSP

This project is a continuation of the procedural generation work initiated by **Erel Segal-Halevi**, specifically extending the concepts introduced in the `CaveGenerator.cs` file. The original `CaveGenerator.cs` utilized **Cellular Automata** for generating natural cave-like structures.

The current implementation introduces `RoomGenerator.cs` and `TileMapRoomGenerator.cs`, which employ **Binary Space Partitioning (BSP)** to create structured dungeon-like environments. This approach complements the original cave generation, offering a more logical and predictable way to generate multi-room maps suitable for dungeons, buildings, or other structured spaces.

## RoomGenerator.cs

### Purpose
`RoomGenerator.cs` implements the **Binary Space Partitioning (BSP)** algorithm to recursively divide a large rectangular space into smaller subspaces and carve rooms into these spaces.

### Features
- **Binary Space Partitioning**:
  - Recursively splits the map into smaller sections, either horizontally or vertically.
  - Split probability and minimum room size parameters determine the partitioning strategy.
  
- **Room Placement**:
  - Ensures rooms are created within the leaf nodes of the BSP tree.
  - Rooms are slightly smaller than their containing spaces to provide walkable areas with walls surrounding them.
  
- **Connectivity**:
  - Automatically connects rooms using corridors, ensuring the entire dungeon is navigable.

### How It Works
1. **Recursive Partitioning**: The space is divided into smaller sections until each section is smaller than the minimum size or no further splits are possible.
2. **Room Creation**: Rooms are placed within the leaf nodes of the BSP tree, with padding to ensure no obstructions.
3. **Corridor Linking**: Rooms are linked via corridors to create a fully connected dungeon.

---

## TileMapRoomGenerator.cs

### Purpose
`TileMapRoomGenerator.cs` converts the logical room data from `RoomGenerator.cs` into a tile-based map. This allows the procedural dungeon layout to be rendered and used in a game engine.

### Features
- **Tile-Based Conversion**: Transforms room and corridor data into a 2D grid of tiles for rendering.
- **Customizable Tiles**: Supports defining specific tiles for walls, floors, and corridors.
- **Seamless Integration**: Works in conjunction with `RoomGenerator.cs` to produce complete, playable maps.

---

## Binary Space Partitioning (BSP)

### What is BSP?
Binary Space Partitioning (BSP) is a recursive algorithm used to divide a space into smaller subspaces in a hierarchical manner. It is commonly used in games for structured map generation, such as dungeons or multi-room environments.

### How BSP Works
1. **Splitting**: A large space is divided into two subspaces, either horizontally or vertically.
2. **Recursion**: Each subspace is further divided until reaching the desired granularity.
3. **Room Creation**: Rooms are carved into the smallest subspaces (leaf nodes of the BSP tree).
4. **Connectivity**: Corridors connect the rooms to ensure navigability.

### Advantages of BSP
- Produces structured layouts with logical room placement.
- Ensures all rooms are connected via corridors.
- Offers fine-tuned control over room size, density, and layout.

---

## Comparison: BSP vs. Cellular Automata

| Feature                         | Binary Space Partitioning (BSP)     | Cellular Automata (CaveGenerator) |
|----------------------------------|-------------------------------------|-----------------------------------|
| **Structure**                    | Predictable, grid-based            | Organic, irregular               |
| **Algorithm**                    | Recursive tree structure           | Iterative smoothing              |
| **Room Connectivity**            | Guaranteed with corridors          | Not guaranteed (requires extra steps) |
| **Use Case**                     | Dungeons, buildings                | Caves, natural landscapes        |
| **Output Appearance**            | Logical rooms and corridors        | Random, natural cave-like patterns |

---

## Integration with Erel Segal-Halevi's CaveGenerator.cs

The original `CaveGenerator.cs` by **Erel Segal-Halevi** used **Cellular Automata** to generate natural cave-like environments. This project builds on that foundation by introducing structured map generation techniques.

- `CaveGenerator.cs`: Creates natural cave-like environments using Cellular Automata.
- `RoomGenerator.cs` and `TileMapRoomGenerator.cs`: Generate structured dungeons with connected rooms and corridors.

By combining these techniques, developers can create hybrid maps that blend natural caves with man-made dungeons, providing diverse and engaging environments for games.

---

## Future Directions

- **Hybrid Map Generation**: Combine BSP and Cellular Automata to create maps that transition between structured rooms and natural caves.
- **Dynamic Content**: Add interactive elements such as traps, treasure, and enemies to the generated maps.
- **Room Types**: Implement specialized rooms such as treasure chambers, boss arenas, and secret passageways.

This project demonstrates the power of procedural generation techniques, blending structure and organic randomness to create immersive and varied game worlds.

---

## Acknowledgments
This project builds upon the procedural generation work of **Erel Segal-Halevi**, specifically extending the `CaveGenerator.cs` implementation to include structured environments through Binary Space Partitioning.

---

Feel free to adapt this template as needed for your repository!
