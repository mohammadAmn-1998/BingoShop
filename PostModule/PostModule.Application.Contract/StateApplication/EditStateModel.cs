﻿namespace PostModule.Application.Contract.StateApplication
{
    public class EditStateModel : CreateStateModel
    {
        public int Id { get; set; }
        public List<int>? CloseStates { get; set; }
    }

}
