using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct BlockBrick
{
    public BrickType Type;
    public Vector2Int Dimension;

    public int CountBricks {get {return Dimension.x * Dimension.y;}}
}

public enum BrickType
{
    Brick_1x1,
    Brick_2x1,
    Brick_2x2,
    Brick_2x4,
}

public struct TransformBlock
{
    public BlockBrick Block;
    public Vector3 Position;
}

public class ParseBricks
{
    public float NeighborDistance = 1.0f;

    public BlockBrick[] BlockBricks = 
    {
      new BlockBrick(){
        Type = BrickType.Brick_1x1,
        Dimension = new Vector2Int(1, 1),
      },
      new BlockBrick(){
        Type = BrickType.Brick_2x1,
        Dimension = new Vector2Int(2, 1),
      },
      new BlockBrick(){
        Type = BrickType.Brick_2x2,
        Dimension = new Vector2Int(2, 2),
      },
      new BlockBrick(){
        Type = BrickType.Brick_2x4,
        Dimension = new Vector2Int(2, 4),
      }
    };

    public BlockBrick SelectRandomBlock()
    {
        return BlockBricks[Random.Range(1, BlockBricks.Length - 1)];
    }
    

    public void Parse(List<Transform> transforms)
    {
        Random.InitState(0);

        List<TransformBlock> transformBlocks = new List<TransformBlock>();
        var currentBrick = transforms[0];
        var bricks = transforms.GetRange(1, transforms.Count - 1);

        for(int indexCurrentBrick = 0; bricks.Count > 0;)
        {
            var preferedBlock = SelectRandomBlock();

            List<Transform> areaBlock = BlockAvailable(bricks, currentBrick, preferedBlock);

            if(areaBlock != null)
            {
                transformBlocks.Add(new TransformBlock(){Block = preferedBlock, Position = currentBrick.position});
                foreach (var b in areaBlock) bricks.Remove(b);
            }
            else
            {
                transformBlocks.Add(new TransformBlock(){Block = BlockBricks[0], Position = currentBrick.position});
                bricks.Remove(currentBrick);
                indexCurrentBrick++;
            }
            currentBrick = transforms[indexCurrentBrick];
        }
    }

    public List<Transform> BlockAvailable(List<Transform> bricks, Transform transform, BlockBrick block)
    {
        List<Transform> blockPositions = new List<Transform> {transform};

        foreach (var brick in bricks)
        {
            var neighborBrickPosition = brick.position - transform.position;

            if(PositionInsideBlock(neighborBrickPosition, block))
            {
                blockPositions.Add(brick);
            }

            if(blockPositions.Count == block.CountBricks) return blockPositions;
        }

        return null;
    }

    public bool PositionInsideBlock(Vector3 position, BlockBrick block)
    {
        return position.x > 0 && position.x < block.Dimension.x && 
            position.y > 0 && position.y < block.Dimension.y;
    }
}