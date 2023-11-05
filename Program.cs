using System;
using System.Collections.Generic;
using System.Text;

class FarmerProblem
{
    // Biểu diễn trạng thái của bài toán
    class State
    {
        public int Farmer { get; set; }
        public int Wolf { get; set; }
        public int Sheep { get; set; }
        public int Cabbage { get; set; }

        public State(int farmer, int wolf, int sheep, int cabbage)
        {
            Farmer = farmer;
            Wolf = wolf;
            Sheep = sheep;
            Cabbage = cabbage;
        }

        public bool IsGoal()
        {
            return Farmer == 0 && Wolf == 0 && Sheep == 0 && Cabbage == 0;
        }

        public bool IsValid()
        {
            // Kiểm tra xem trạng thái hiện tại có hợp lệ không
            if ((Wolf == Sheep && Farmer != Wolf) || (Sheep == Cabbage && Farmer != Sheep))
                return false;

            return true;
        }

        public override string ToString()
        {
            return $"Người nông dân: {Farmer}, Sói: {Wolf}, Cừu: {Sheep}, Bắp cải: {Cabbage}";
        }
    }

    static void DFS(State state, Stack<State> path, HashSet<string> visitedStates)
    {
        visitedStates.Add(state.ToString());

        if (state.IsGoal())
        {
            foreach (var s in path)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Đã tìm thấy giải pháp.");
            return;
        }

        int farmer = state.Farmer;
        int wolf = state.Wolf;
        int sheep = state.Sheep;
        int cabbage = state.Cabbage;

        int nextFarmer = 1 - farmer;

        Move(new State(nextFarmer, wolf, sheep, cabbage));
        if (farmer == wolf)
        {
            Move(new State(nextFarmer, nextFarmer, sheep, cabbage));
        }
        if (farmer == sheep)
        {
            Move(new State(nextFarmer, wolf, nextFarmer, cabbage));
        }
        if (farmer == cabbage)
        {
            Move(new State(nextFarmer, wolf, sheep, nextFarmer));
        }

        void Move(State nextState)
        {
            if (!visitedStates.Contains(nextState.ToString()) && nextState.IsValid())
            {
                path.Push(nextState);
                DFS(nextState, path, visitedStates);
                path.Pop();
            }
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        State initialState = new State(1, 1, 1, 1);
        Stack<State> path = new Stack<State>();
        HashSet<string> visitedStates = new HashSet<string>();

        path.Push(initialState);
        DFS(initialState, path, visitedStates);
    }
}
