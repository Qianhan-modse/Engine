﻿using InfiniteMemories.OverWitch.qianhan.Log.lang.logine;
using InfiniteMemories.OverWitch.qianhan.Numbers;

namespace Assets.OverWitch.qianhan.Log
{
    public class StackTraceElement : Serializable
    {
        private string declaringClass;
        private string methodName;
        private string fileName;
        private int lineNumber;
        public StackTraceElement(String declaringClass, String methodName,
                             String fileName, int lineNumber)
        {
            this.declaringClass = Objects.requireNonNull(declaringClass, "Declaring class is null");
            this.methodName = Objects.requireNonNull(methodName, "Method name is null");
            this.fileName = fileName;
            this.lineNumber = lineNumber;
        }
        public string getFileName() { return fileName; }
        public int getLineNumber() { return lineNumber; }
        public string getClassName() { return declaringClass; }
        public string getMethodName() { return methodName; }
        public bool isNativeMethod() { return lineNumber == -2; }
        public string toString()
        {
            return getClassName() + "." + methodName + (isNativeMethod() ? "(Native Method)" : (fileName != null && lineNumber >= 0
                ? "(" + fileName + "." + lineNumber + ")" : (fileName != null ? "(" + fileName + ")" : "(Unknown Source)")));
        }
    }
}
