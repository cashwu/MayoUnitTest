using System;

namespace Lab01;

public class DateUtility
{
    /// <summary>
    /// 已經有人使用，不可以影響到已經用你的人，不可修改參數
    /// 盡可能不要新增 public/internal 的對外介面
    /// </summary>
    /// <returns></returns>
    public bool IsPayday()
    {
        var today = GetToday();

        if (today.Day is 5 or 20)
        {
            return true;
        }

        return false;
    }

    protected virtual DateTime GetToday()
    {
        return DateTime.Today;
    }
}