/// <summary>
/// This class handles indexing and storage of keywords.
/// </summary>
public class InvertedIndex
{
    private Dictionary<string, List<int>> index; // keyword : documentId pair

    /// <summary>
    /// Constructs InvertedIndex and instantiates index
    /// </summary>
    public InvertedIndex()
    {
        index = new Dictionary<string, List<int>>();
    }

    /// <summary>
    /// This method adds document to the indexer
    /// </summary>
    /// <param name="doc">The document to add</param>
    /// <param name="stopWords">The stop words contained in the document</param>
    public void AddDocument(Document doc, StopWords stopWords)
    {
        // split the document to List
        string[] words = doc.Content.Split(new[] { ' ', '.', ',', '!', '?', ';', ':', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var word in words)
        {
            string keyword = word.ToLower();

            if (stopWords.IsStopWord(keyword)) continue;

            if (!index.ContainsKey(keyword))
            {
                index[keyword] = new List<int>();
            }
            
            if (!index[keyword].Contains(doc.Id))
            {
                index[keyword].Add(doc.Id);
            }
        }
    }

    /// <summary>
    ///     This method returns List of documentId that contains the keyword
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns>Returns List of documentsId</returns>
    public List<int> Search(string keyword)
    {
        keyword = keyword.ToLower();

        if (index.ContainsKey(keyword))
        {
            return index[keyword];
        }
        return new List<int>();
    }
}
