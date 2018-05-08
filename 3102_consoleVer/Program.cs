using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3102_consoleVer
{
    class Program
    {
        static void Main(string[] args)
        {
            string encryptionKey; //암호키
            string plainText; //평문
            //string distinct; //중복문자 제거한 문자열 
            char[,] cipher_plate = new char[5, 5]; //암호판
            bool isaddX = false; //홀수여서 X추가해줬는지 아닌지 여부

            Console.Write("암호키를 입력하세요 >>");
            encryptionKey = Console.ReadLine();
            Console.Write("평문을 입력하세요 >>");
            plainText = Console.ReadLine();
            List<char> removeDuplicates = new List<char>(encryptionKey);
            for (int i = (int)'a'; i < (int)'z'; i++)
            {
                removeDuplicates.Add((char)i);
            }
            removeDuplicates = removeDuplicates.Distinct().ToList();
            int tmpCnt = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cipher_plate[i, j] = removeDuplicates[tmpCnt++]; //암호판 완성 
                }
            }

            //암호판 출력
            Console.WriteLine("암호판 출력");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(cipher_plate[i, j]);
                }
                Console.WriteLine();
            }

            //평문 띄어쓴거 없애고 SS같은거 있으면 중간에 X넣기
            List<char> splitPlainText = new List<char>(plainText);
            List<int> rememberIndex = new List<int>(); //X추가해준 index 기억
            do
            {
                splitPlainText.Remove(' ');
            }
            while (splitPlainText.Contains(' '));

            tmpCnt = 0;
            while (true)
            {
                if (splitPlainText[tmpCnt] == splitPlainText[tmpCnt + 1])
                {
                    splitPlainText.Insert(tmpCnt + 1, 'x');
                    rememberIndex.Add(tmpCnt+1);
                }
                tmpCnt += 2;
                if (tmpCnt == splitPlainText.Count() - 1 || tmpCnt >= splitPlainText.Count()) break;
            }

            //홀수면 x 추가해주기
            if (splitPlainText.Count() % 2 == 1)
            {
                splitPlainText.Add('x'); 
                isaddX = true;
            }

            for(int i = 0; i < splitPlainText.Count; i++)
            {
                if (splitPlainText[i].Equals('z'))
                    splitPlainText[i] = 'q';
            } //암호화활 문자열에 z가 있으면 q로 바꿔줌 (q랑z를 같은곳에 둔것처럼 할 수 있음)

            //암호문 생성
           
            List<char> cipher_text = new List<char>();
            int coltemp1 = 0, rowtemp1 = 0, coltemp2 = 0, rowtemp2 = 0;
            for (int i = 0; i < splitPlainText.Count(); i += 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                       
                        if (cipher_plate[j, k] == splitPlainText[i])
                        {
                          
                            coltemp1 = j;
                            rowtemp1 = k;
                        }
                        if (cipher_plate[j, k] == splitPlainText[i + 1])
                        {
                            coltemp2 = j;
                            rowtemp2 = k;
                        }
                        //바꿀문자에 z있으면 Q랑같은 컬럼,열 번호 넣어주고 비교하기
                        //그냥 splitPlainText[i]에 z있으면 q로바꿔줌 
                    }
                }
                //같은행, 같은열에 있을때 처리 
                if(coltemp1 == coltemp2)
                {
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp1, rowtemp1 + 1]);
                       
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[coltemp1, 0]);
                        
                    }
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp2, rowtemp2 + 1]);
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[coltemp2, 0]);
                    }
                    continue;
                }
                if (rowtemp1 == rowtemp2)
                {
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp1 + 1, rowtemp1]);
                    
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[0, rowtemp1]);
                    }
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp2 + 1, rowtemp2]);
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[0, rowtemp2]);
                    }
                   
                    continue;
                }
                cipher_text.Add(cipher_plate[coltemp2,rowtemp1]);
                cipher_text.Add(cipher_plate[coltemp1, rowtemp2]);
            } //81 ~153라인함수로 만들 수 있을듯?그래서 복호화랑 암호화랑 같이 진행하자!
            Console.WriteLine("암호문 출력");
            foreach (char item in cipher_text)
            {
                Console.Write(item);
            }
            //복호화하기
            List<char> plain_text = new List<char>();
            for (int i = 0; i < cipher_text.Count(); i += 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (cipher_plate[j, k] == cipher_text[i])
                        {

                            coltemp1 = j;
                            rowtemp1 = k;
                        }
                        if (cipher_plate[j, k] == cipher_text[i + 1])
                        {
                            coltemp2 = j;
                            rowtemp2 = k;
                        }
                    }
                }
                if (coltemp1 == coltemp2)
                {
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp1, rowtemp1 -1]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[coltemp1, 4]);

                    }
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp2, rowtemp2 - 1]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[coltemp2, 4]);
                    }
                    continue;
                }
                if (rowtemp1 == rowtemp2)
                {
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp1 -1, rowtemp1]);

                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[4, rowtemp1]);
                    }
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp2 - 1, rowtemp2]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[4, rowtemp2]);
                    }

                    continue;
                }
                plain_text.Add(cipher_plate[coltemp2, rowtemp1]);
                plain_text.Add(cipher_plate[coltemp1, rowtemp2]);
            }

            //홀수일때 추가해준 x삭제
            if (isaddX) {
               
                plain_text.RemoveAt((plain_text.Count()-1));
            }
            //중간에 넣어준 x 삭제
            for (int i = rememberIndex.Count()-1; i >=0; i--)
            {
                plain_text.RemoveAt(rememberIndex[i]);
            }
            Console.WriteLine("\n복호문 출력");
            foreach (char item in plain_text)
            {
                Console.Write(item);
            }
        }

    }
}