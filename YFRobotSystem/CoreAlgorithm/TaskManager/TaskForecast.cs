using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLPublicClass;

namespace CoreAlgorithm.TaskManager
{
    public class TaskForecast
    {
        /// <summary>
        /// list<list<任务id>>
        /// </summary>
        public static List<List<int>> m_TaskForecast; //任务列队

        /// <summary>
        /// 数据类型：Dictionary<任务id, 任务信息> 
        /// </summary>
        public Dictionary<int, TaskModule> m_TaskList;     //任务信息
        public int m_TaskCount = 120;
        public object LockObj = new object();
        TasksManager tm;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_TaskCount">任务计划缓存，默认120秒缓存数据</param>
        public TaskForecast( int _TaskCount = 120)
        {
            m_TaskForecast = new List<List<int>>();
            m_TaskCount = _TaskCount;
            m_TaskList = new Dictionary<int, TaskModule>();
            tm = new TasksManager();
        }

        //制定任务计划
        public void UpdateTaskForecast()
        {
            UpdateTasks(tm.GetTaskClass());
            for (int i = m_TaskForecast.Count; i < m_TaskCount; i++)
            {
                m_TaskForecast.Add(GetTaskQueueItem());
            }
        }

        public List<int> GetTaskQueueItem()
        {
            List<int> iList = new List<int>();
            for (int i = 0; i < m_TaskList.Count; i++)
            {
                if (m_TaskList.ElementAt(i).Value.Next())
                {
                    iList.Add(m_TaskList.ElementAt(i).Key);
                }
            }
            return iList;
        }
        public void UpdateTasks(Dictionary<int, TaskModule> NewTaskList)
        {
            List<TaskModule> NoFind = new List<TaskModule>();
            List<TaskModule> Changed = new List<TaskModule>();
            List<TaskModule> Redundanted = new List<TaskModule>();

            //找出任务信息中没有的 并添加
            for(int i = 0; i < NewTaskList.Count; i++)
            {
                if (m_TaskList.ContainsKey(NewTaskList.ElementAt(i).Key) == false)
                {
                    NoFind.Add(NewTaskList.ElementAt(i).Value);
                    m_TaskList.Add(NewTaskList.ElementAt(i).Key, NewTaskList.ElementAt(i).Value);
                }
            }
            //找出任务信息中多余的 并移除
            for (int i = 0; i < m_TaskList.Count; i++)
            {
                if (NewTaskList.ContainsKey(m_TaskList.ElementAt(i).Key) == false)
                {
                    Redundanted.Add(m_TaskList.ElementAt(i).Value);
                    m_TaskList.Remove(m_TaskList[i].m_Taskid);
                }
            }
            
            //找不一样的 并更新
            for (int i = 0; i < m_TaskList.Count; i++)
            {
                int key = m_TaskList.ElementAt(i).Key;
                NewTaskList[key].m_NumIndex = m_TaskList[key].m_NumIndex;
                if (NewTaskList[key].m_TimeSpace != m_TaskList[key].m_TimeSpace)
                {
                    Changed.Add(NewTaskList[key]);
                    m_TaskList.Remove(NewTaskList[key].m_Taskid);
                    m_TaskList.Add(NewTaskList[key].m_Taskid, NewTaskList[key]);
                }
            }

            //执行任务维护移除多余的
            for (int i = 0; i < Redundanted.Count; i++)
            {
                RemoverTaskToTaskQueue(Redundanted.ElementAt(i));
            }
            //执行任务维护没有的
            for (int i = 0; i < NoFind.Count; i++)
            {
                AddTaskToTaskQueue(NoFind.ElementAt(i));
            }
            //执行任务修改的
            for (int i = 0; i < Changed.Count; i++)
            {
                RemoverTaskToTaskQueue(Changed.ElementAt(i));
                AddTaskToTaskQueue(Changed.ElementAt(i));
            }
        }

        //添加新任务
        public void AddTaskToTaskQueue(TaskModule tm)
        {
            if (tm.m_Taskid <= 0)
                return;
            for (int i = 0; i < m_TaskForecast.Count; i++)
            {
                if (tm.Next())
                {
                    m_TaskForecast[i].Add(tm.m_Taskid);
                }
            }
        }

        public void RemoverTaskToTaskQueue(TaskModule tm)
        {
            if (tm.m_Taskid <= 0)
                return;
            for (int i = 0; i < m_TaskForecast.Count; i++)
            {
                m_TaskForecast[i].Remove(tm.m_Taskid);
            }
        }
    }
}
