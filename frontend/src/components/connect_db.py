# -*- coding: UTF-8 -*-
from os import getenv

import pymssql


def writeFile(path, content):
    with open(path, "w") as fo:
        fo.write(content)


server = getenv("LuPC20181007")
print(getenv("localhost"))

user = getenv("sa")
password = getenv("sa")

with pymssql.connect(".", "sa", "sa", "GeneratorDB") as conn:
    with conn.cursor(as_dict=True) as cursor:
        cursor.execute("select row_number()OVER(ORDER BY tab.id ASC)+34 seq, tab.table_name,tab.table_name_ch,col.column_name,col.column_description,col.dbtype from TableSchema tab join V_Column col on tab.Id=col.VTableComments2_Id where col.column_description like '%数量%'")
        list = cursor.fetchall()
        writeFile("test_seq.csv", "\n".join(
            ["#{seq},填写{table_name_ch},输入高于允许值的{column_description} 99999, 提示{column_description}不够".format(**row) for row in list]))
