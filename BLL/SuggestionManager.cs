using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SuggestionManager
    {
        private SuggestionService suggestionService = new SuggestionService();

        /// <summary>
        /// 提交投诉
        /// </summary>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public int SubmitSuggestion(Suggestion suggestion)
        {
            return suggestionService.SubmitSuggestion(suggestion);
        }
        /// <summary>
        /// 获取最新的建议
        /// </summary>
        /// <returns></returns>
        public List<Suggestion> GetSuggestion()
        {
            return suggestionService.GetSuggestion();
        }
        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <param name="suggestionId"></param>
        /// <returns></returns>
        public int HandleSuggestion(int suggestionId)
        {
            return suggestionService.HandleSuggestion(suggestionId);
        }
    }
}
