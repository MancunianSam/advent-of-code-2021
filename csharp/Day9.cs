// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;

List<int> GetPositions(int value, int maxValue)
{
    var retVal = new List<int>{value+1, value-1};
    return retVal.FindAll(pos => pos >= 0 && pos < maxValue);
}

var text = File.ReadAllText("/home/sam/repositories/advent-of-code-2021/c#/input.txt");
var inputArr = text.Split("\n");
var inputList = inputArr.ToList().Select(el => el.ToCharArray().Select(c => int.Parse(c.ToString())).ToList()).ToList();
var inputCount = inputList.Count;
var innerCount = inputList[0].Count;
var sum = 0;
var sizeList = new List<int>();

List<(int First, int Second)> GetCoordinates(int x, int y)
{
    var xPositions = GetPositions(x, innerCount);
    var yPositions = GetPositions(y, inputCount);
    var coordinates = xPositions.Zip(Enumerable.Repeat(y, xPositions.Count).ToList()).ToList();
    var yCoordinates = Enumerable.Repeat(x, yPositions.Count).Zip(yPositions).ToList();
    coordinates.AddRange(yCoordinates);
    return coordinates;
}

List<(int First, int Second)> GetBasin(List<(int X, int Y)> pointsToCheck, List<(int First, int Second)> checkedPoints)
{
    var filteredPoints = pointsToCheck.FindAll(
        point => 
            inputList[point.Y][point.X] != 9 && !checkedPoints.Contains(point));
    if (filteredPoints.Count == 0) return checkedPoints;
    {
        var newPoints = new List<(int First, int Second)>();
        filteredPoints.ForEach(point => newPoints.AddRange(GetCoordinates(point.X, point.Y)));
        checkedPoints.AddRange(filteredPoints);
        return GetBasin(newPoints.Distinct().ToList(), checkedPoints);
    }
}

for (var y = 0; y < inputCount; y++)
{
    for (var x = 0; x < innerCount; x++)
    {
        var currentValue = inputList[y][x];
        var coordinates = GetCoordinates(x, y);
        var coordinateValues = coordinates.Select(c => inputList[c.Second][c.First]).ToList();
        var lowerCount = coordinateValues.FindAll(each => each <= currentValue).Count;
        if (lowerCount == 0)
        {
            // Console.WriteLine("({0}, {1}) {2}", x, y, currentValue);
            var basin = GetBasin(coordinates, new List<(int First, int Second)>{(x, y)});
            sizeList.Add(basin.Count);
            sum = sum + 1 + currentValue;
        }
    }
}

Console.WriteLine("Sum of risk levels {0}", sum);
sizeList.Sort();
sizeList.Reverse();
var product  = sizeList.GetRange(0, 3).Aggregate(1, (x, y) => x * y);
Console.WriteLine("Product of basin sizes {0}", product);  

