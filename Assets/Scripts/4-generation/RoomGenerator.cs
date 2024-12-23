using System;
using System.Collections.Generic;

public class RoomGenerator {
    private int mapWidth;
    private int mapHeight;
    private int minRoomSize;
    private Random random;
    
    private int corridorMargin = 1;

    private float probability = 0.5f;

    private float threshold = 1.25f;
    private int roomThick = 2;
    public int[,] Map { get; private set; }

    public RoomGenerator(int width, int height, int minRoomSize = 6 , float probabilitySplit = 0.5f , float thresholdSplit = 1.25f,int wallThick = 2) {
        mapWidth = width;
        mapHeight = height;
        probability = probabilitySplit;
        roomThick = wallThick;
        threshold = thresholdSplit;
        this.minRoomSize = minRoomSize;
        Map = new int[width, height];
        random = new Random();
    }

    public void GenerateMap() {
        // Clear the map
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                Map[x, y] = 1; // Initialize everything as walls
            }
        }

        // Create the root node for the BSP tree
        BSPNode rootNode = new BSPNode(0, 0, mapWidth, mapHeight);

        // Split the map into rooms
        SplitNode(rootNode);

        // Fill the rooms in the map
        List<BSPNode> rooms = rootNode.GetRooms(roomThick);
        foreach (var room in rooms) {
            FillRoom(room);
        }

        // Connect rooms with corridors
        ConnectRooms(rooms);
    }

    private void SplitNode(BSPNode node) {
        if (!node.Split(minRoomSize , probability,threshold)) return;

        SplitNode(node.Left);
        SplitNode(node.Right);
    }

    private void FillRoom(BSPNode node) {
        for (int x = node.RoomX; x < node.RoomX + node.RoomWidth; x++) {
            for (int y = node.RoomY; y < node.RoomY + node.RoomHeight; y++) {
                Map[x, y] = 0; // Mark as floor
            }
        }
    }

    private void ConnectRooms(List<BSPNode> rooms) {
        for (int i = 1; i < rooms.Count; i++) {
            BSPNode roomA = rooms[i - 1];
            BSPNode roomB = rooms[i];

            int pointAX = random.Next(roomA.RoomX, roomA.RoomX + roomA.RoomWidth);
            int pointAY = random.Next(roomA.RoomY, roomA.RoomY + roomA.RoomHeight);
            int pointBX = random.Next(roomB.RoomX, roomB.RoomX + roomB.RoomWidth);
            int pointBY = random.Next(roomB.RoomY, roomB.RoomY + roomB.RoomHeight);

            CreateCorridor(pointAX, pointAY, pointBX, pointBY);
        }
    }

    private void CreateCorridor(int x1, int y1, int x2, int y2) {
        while (x1 != x2) {
            Map[x1, y1] = 0; // Mark as floor
            x1 += x1 < x2 ? corridorMargin : -corridorMargin;
        }

        while (y1 != y2) {
            Map[x1, y1] = 0; // Mark as floor
            y1 += y1 < y2 ? corridorMargin : -corridorMargin;
        }
    }
}

public class BSPNode {
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public BSPNode Left { get; private set; }
    public BSPNode Right { get; private set; }

    public int RoomX { get; private set; }
    public int RoomY { get; private set; }
    public int RoomWidth { get; private set; }
    public int RoomHeight { get; private set; }

    private Random random;

    public BSPNode(int x, int y, int width, int height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        random = new Random();
    }

    public bool Split(int minSize , float probability , float threshold) {
        if (Width < minSize * 2 && Height < minSize * 2) return false;

        bool splitHorizontally = random.NextDouble() > probability;
        if (Width > Height && Width / Height >= threshold) splitHorizontally = false;
        else if (Height > Width && Height / Width >= threshold) splitHorizontally = true;

        int max = (splitHorizontally ? Height : Width) - minSize;
        if (max <= minSize) return false;

        int split = random.Next(minSize, max);

        if (splitHorizontally) {
            Left = new BSPNode(X, Y, Width, split);
            Right = new BSPNode(X, Y + split, Width, Height - split);
        } else {
            Left = new BSPNode(X, Y, split, Height);
            Right = new BSPNode(X + split, Y, Width - split, Height);
        }

        return true;
    }

    public List<BSPNode> GetRooms(int roomThick) {
        if (Left == null && Right == null) {
            RoomX = X + 1;
            RoomY = Y + 1;
            RoomWidth = Width - roomThick;
            RoomHeight = Height - roomThick;
            return new List<BSPNode> { this };
        }

        List<BSPNode> rooms = new List<BSPNode>();
        if (Left != null) rooms.AddRange(Left.GetRooms(roomThick));
        if (Right != null) rooms.AddRange(Right.GetRooms(roomThick));
        return rooms;
    }
}
