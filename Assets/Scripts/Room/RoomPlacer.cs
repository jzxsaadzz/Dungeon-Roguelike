﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomsPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room StartingRoom;
    public Room BossRoomPrefab;
    public Room[,] spawnedRooms;

    private IEnumerator Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        for (int i = 0; i < 12; i++)
        {
            // Это вот просто убрать чтобы подземелье генерировалось мгновенно на старте
            yield return new WaitForSecondsRealtime(0.001f);

            PlaceOneRoom();
        }
        Vector2Int bossRoomPosition = FindFarthestRoom();
        GenerateBossRoom(bossRoomPosition);
    }

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        // Эту строчку можно заменить на выбор комнаты с учётом её вероятности, вроде как в ChunksPlacer.GetRandomChunk()
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5)* 31,(position.y - 5) * 21, 0);
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if(selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(true);
            selectedRoom.DoorD.SetActive(true);


        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(true);
            selectedRoom.DoorU.SetActive(true);

        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(true);
            selectedRoom.DoorL.SetActive(true);

        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(true);
            selectedRoom.DoorR.SetActive(true);

        }

        return true;
    }


    private Vector2Int FindFarthestRoom()
    {
        Vector2Int farthestRoom = Vector2Int.zero;
        int maxDistance = 0;

        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y]!= null)
                {
                    int distance = Mathf.Abs(x - 5) + Mathf.Abs(y - 5);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        farthestRoom = new Vector2Int(x, y);
                    }
                }
            }
        }

        return farthestRoom;
    }

private void GenerateBossRoom(Vector2Int position)
{
    // Destroy the existing room at the boss room position
    if (spawnedRooms[position.x, position.y] != null)
    {
        Destroy(spawnedRooms[position.x, position.y].gameObject);
        spawnedRooms[position.x, position.y] = null;
    }

    Room bossRoom = Instantiate(BossRoomPrefab);
    bossRoom.transform.position = new Vector3((position.x - 5) * 31, (position.y - 5) * 21, 0);
    spawnedRooms[position.x, position.y] = bossRoom;
    ConnectToSomething(bossRoom,position);
    ChangeAdjacentRoomDoorSprite(position);
}

    

private void ChangeAdjacentRoomDoorSprite(Vector2Int bossRoomPosition)
{
    int maxX = spawnedRooms.GetLength(0) - 1;
    int maxY = spawnedRooms.GetLength(1) - 1;

    List<Vector2Int> neighbours = new List<Vector2Int>();

    if (bossRoomPosition.y < maxY && spawnedRooms[bossRoomPosition.x, bossRoomPosition.y + 1] != null) neighbours.Add(Vector2Int.up);
    if (bossRoomPosition.y > 0 && spawnedRooms[bossRoomPosition.x, bossRoomPosition.y - 1] != null) neighbours.Add(Vector2Int.down);
    if (bossRoomPosition.x < maxX && spawnedRooms[bossRoomPosition.x + 1, bossRoomPosition.y] != null) neighbours.Add(Vector2Int.right);
    if (bossRoomPosition.x > 0 && spawnedRooms[bossRoomPosition.x - 1, bossRoomPosition.y] != null) neighbours.Add(Vector2Int.left);

    foreach (Vector2Int neighbour in neighbours)
    {
        Room adjacentRoom = spawnedRooms[bossRoomPosition.x + neighbour.x, bossRoomPosition.y + neighbour.y];
        if (neighbour == Vector2Int.up && adjacentRoom.DoorD != null) adjacentRoom.DoorD.GetComponent<SpriteRenderer>().sprite = BossRoomPrefab.DoorU.GetComponent<SpriteRenderer>().sprite;
        if (neighbour == Vector2Int.down && adjacentRoom.DoorU != null) adjacentRoom.DoorU.GetComponent<SpriteRenderer>().sprite = BossRoomPrefab.DoorD.GetComponent<SpriteRenderer>().sprite;
        if (neighbour == Vector2Int.right && adjacentRoom.DoorL != null) adjacentRoom.DoorL.GetComponent<SpriteRenderer>().sprite = BossRoomPrefab.DoorR.GetComponent<SpriteRenderer>().sprite;
        if (neighbour == Vector2Int.left && adjacentRoom.DoorR != null) adjacentRoom.DoorR.GetComponent<SpriteRenderer>().sprite = BossRoomPrefab.DoorL.GetComponent<SpriteRenderer>().sprite;
    }
}


}