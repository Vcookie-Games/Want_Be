using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using ReuseSystem;
using UnityEngine;

public class ChunkGeneration : Singleton<ChunkGeneration>
{
    [SerializeField] private List<Chunk> chunkPatterns;
    //greater or equal to 2
    [SerializeField] private int numberOfChunkLoad;

    private Chunk[] chunksRecent;
    private Chunk firstChunk;
    private Chunk secondChunk;

    private Chunk destroyableChunk;
    
    Chunk GetRandomChunkPattern()
    {
        return chunkPatterns[Random.Range(0, chunkPatterns.Count)];
    }

    public void InitGenerateChunk(Vector2 startPosition)
    {
        chunksRecent = new Chunk[numberOfChunkLoad];
        chunksRecent[0] = GenerateNewChunk(startPosition);
        for (int i = 1; i < numberOfChunkLoad; i++)
        {
            chunksRecent[i] = GenerateNewChunk(chunksRecent[i - 1].GetTopPosition());
        }
        //firstChunk = GenerateNewChunk(startPosition);
        //secondChunk = GenerateNewChunk(firstChunk.GetTopPosition());
    }

    Chunk GenerateNewChunk(Vector2 position)
    {
        var chunk = ReuseSystem.SimplePool.Instance.Spawn<Chunk>(GetRandomChunkPattern(),position);
        var currentScale = chunk.transform.localScale;
        currentScale.x = GameController.Instance.GetAspectCompareToNormalScreen();
        chunk.transform.localScale = currentScale;
        return chunk;
    }

    public void GenerateNextChunk()
    {
        if (destroyableChunk != null)
        {
            ReuseSystem.SimplePool.Instance.Despawn(destroyableChunk);
        }

        destroyableChunk = chunksRecent[0];
        for (int i = 0; i < numberOfChunkLoad - 1; i++)
        {
            chunksRecent[i] = chunksRecent[i + 1];
        }

        chunksRecent[numberOfChunkLoad - 1] = GenerateNewChunk(chunksRecent[numberOfChunkLoad - 2].GetTopPosition());
        //firstChunk = secondChunk;
        //secondChunk = GenerateNewChunk(firstChunk.GetTopPosition());
    }

    public void CheckUpdateNextChunk(float positionY)
    {
        if (positionY <= chunksRecent[0].GetTopPosition().y) return;
        GenerateNextChunk();
    }
}
