using System.Diagnostics;
using System.IO;
using System.Text;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;
using BlockSlideCore.Utilities;

namespace BlockSlideCore.Analysis
{
    public class DotFileGenerator
    {
        public void GenerateDotFile(Level level, string directory)
        {
            Directory.CreateDirectory(directory);
            var timer = new Stopwatch();
            timer.Start();
            var dotFileContents = GenerateDotFileContents(level);
            File.WriteAllText(string.Format("{0}/Level{1}.dot", directory, level.LevelNumber),
                dotFileContents);
            timer.Stop();
            Debug.WriteLine("Took {0}ms to create the level graph.", timer.ElapsedMilliseconds);
        }

        public void GenerateBatchConvertFile(Level level, string directory, string dotExePath)
        {
            var command = string.Format("dot -Tpng Level{0}.dot > Level{0}.png", level.LevelNumber);
            File.WriteAllText(string.Format("{0}/convert.bat", directory),
                string.Format(@"{0}\{1}", dotExePath, command));
        }

        private string GenerateDotFileContents(Level level)
        {
            var graphBuilder = new GraphBuilder();
            var root = graphBuilder.BuildGraph(level);
            var dotFileDataBuilder = new StringBuilder();
            dotFileDataBuilder.AppendLine("digraph G {");
            dotFileDataBuilder.AppendLine(string.Format("\"{0}\" [color=green,style=filled]", level.StartLocation));
            dotFileDataBuilder.AppendLine(string.Format("\"{0}\" [color=red,style=filled]", level.FinishLocation));
            root.DepthFirstSearch(node =>
                node.Neighbors.ForEach(neighbor =>
                    dotFileDataBuilder.AppendLine(string.Format("\"{0}\" -> \"{1}\";", node.Value, neighbor.Value))));
            dotFileDataBuilder.AppendLine("}");
            return dotFileDataBuilder.ToString();
        }
    }
}
