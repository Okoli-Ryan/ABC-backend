namespace ABC {
    public class NewsService {

        NetworkService netService;
        Dictionary<string, string> NewsTypes = new Dictionary<string, string>() {
            {"top", "https://hacker-news.firebaseio.com/v0/topstories.json" },
            {"new", "https://hacker-news.firebaseio.com/v0/newstories.json" },
            {"ask", "https://hacker-news.firebaseio.com/v0/askstories.json" },
            {"show", "https://hacker-news.firebaseio.com/v0/showstories.json" },
            {"job", "https://hacker-news.firebaseio.com/v0/jobstories.json" },
        };

        public NewsService(NetworkService netService) {
            this.netService = netService;
        }

        public async Task<List<int>> GetStoryIDs(string Type, int Start = 0) {

            List<int> storyIDs = await netService.HttpGet<List<int>>(NewsTypes[Type]);


            return storyIDs.Skip(Start).Take(10).ToList();

        }

        public async Task<List<News>> GetStories(string Type, int Start = 0) {

            List<int> storyIDs = await GetStoryIDs(Type, Start);

            List<Task<News>> tasks = new();

            storyIDs.ForEach(x => tasks.Add(Task.Run(() => netService.HttpGet<News>($"https://hacker-news.firebaseio.com/v0/item/{x}.json"))));

            var taskList = await Task.WhenAll(tasks);

            return taskList.Select(r => (News)r).ToList();
        }

        public async Task<List<News>> GetComments(List<int> storyIDs) {


            List<Task<News>> tasks = new();

            storyIDs.ForEach(x => tasks.Add(Task.Run(() => netService.HttpGet<News>($"https://hacker-news.firebaseio.com/v0/item/{x}.json"))));

            var taskList = await Task.WhenAll(tasks);

            return taskList.Select(r => (News)r).ToList();
        }

    }
}
