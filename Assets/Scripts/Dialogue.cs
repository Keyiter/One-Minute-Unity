[System.Serializable]
public class Dialogue
{
    public Part[] parts;
    
    public Part getPart(string id) {
        return System.Array.Find(parts, part => part.ID == id);
    }

    [System.Serializable]
    public class Part
    {
        public string ID;
        public string text;
        public string nextId;
        public Response[] responses;

        [System.Serializable]
        public class Response
        {
            public string text;
            public string ID;
        }
    }
}
