﻿namespace WikiDown.Security
{
    public enum ArticleAccessLevel
    {
        Anonymous = 0,

        LoggedIn = 1,

        Editor = 10,

        SuperUser = 20,

        Admin = 30,

        Root = 100
    }
}