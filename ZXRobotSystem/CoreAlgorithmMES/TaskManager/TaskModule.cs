using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreAlgorithm.TaskManager
{
    public class TaskModule
    {
        public int m_Taskid;
        
        public int m_NumIndex;
        public string m_TaskName;
        public DateTime m_TaskCreatTime;
        public string m_TaskCreatUser;
        public string m_TaskStats;
        public int m_TimeSpace;
       

        public TaskModule()
        {
            m_NumIndex = 0;
        }

        public bool Next()
        {
            m_NumIndex++;
            if (m_NumIndex == m_TimeSpace)
            {
                m_NumIndex=0;
                return true;
            }
            return false;
        }
    }    
}
