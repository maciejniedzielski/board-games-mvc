namespace BoardGames.ViewModels
{
    public class IndexHeaderViewModel
    {
        public string ControllerName { get; set; }
        
        public IndexHeaderViewModel(string controllerName)
        {
            ControllerName = controllerName;
        }
    }
}