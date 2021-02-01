using System;
using System.Globalization;
using System.Text;

namespace DGP.Snap.AutoVersion
{
    internal class VersionEx : ICloneable, IComparable, IComparable<VersionEx>, IEquatable<VersionEx>
    {
        internal enum ParseFailureKind
        {
            ArgumentNullException,
            ArgumentException,
            ArgumentOutOfRangeException,
            FormatException
        }

        internal struct VersionResult
        {
            internal VersionEx m_parsedVersion;

            internal ParseFailureKind m_failure;

            internal string m_exceptionArgument;

            internal string m_argumentName;

            internal bool m_canThrow;

            internal void Init(string argumentName, bool canThrow)
            {
                m_canThrow = canThrow;
                m_argumentName = argumentName;
            }

            internal void SetFailure(ParseFailureKind failure)
            {
                SetFailure(failure, string.Empty);
            }

            internal void SetFailure(ParseFailureKind failure, string argument)
            {
                m_failure = failure;
                m_exceptionArgument = argument;
                if (m_canThrow)
                {
                    throw GetVersionParseException();
                }
            }

            internal Exception GetVersionParseException()
            {
                switch (m_failure)
                {
                    case ParseFailureKind.ArgumentNullException:
                        return new ArgumentNullException(m_argumentName);
                    case ParseFailureKind.ArgumentException:
                        return new ArgumentException(/*Environment.GetResourceString("Arg_VersionString")*/);
                    case ParseFailureKind.ArgumentOutOfRangeException:
                        return new ArgumentOutOfRangeException(m_exceptionArgument/*, Environment.GetResourceString("ArgumentOutOfRange_Version")*/);
                    case ParseFailureKind.FormatException:
                        try
                        {
                            int.Parse(m_exceptionArgument, CultureInfo.InvariantCulture);
                        }
                        catch (FormatException result)
                        {
                            return result;
                        }
                        catch (OverflowException result2)
                        {
                            return result2;
                        }

                        return new FormatException(/*Environment.GetResourceString("Format_InvalidString")*/);
                    default:
                        return new ArgumentException(/*Environment.GetResourceString("Arg_VersionString")*/);
                }
            }
        }

        private int _Major;
        private int _Minor;
        private int _Build = -1;
        private int _Revision = -1;
        private static readonly char[] SeparatorsArray = new char[1]
        {
            '.'
        };
        private const int ZERO_CHAR_VALUE = 48;

        public int Major
        {
            get
            {
                return _Major;
            }
            set
            {
                _Major = value;
            }
        }
        public int Minor
        {
            get
            {
                return _Minor;
            }
            set
            {
                _Minor = value;
            }
        }
        public int Build
        {
            get
            {
                return _Build;
            }
            set
            {
                _Build = value;
            }
        }
        public int Revision
        {
            get
            {
                return _Revision;
            }
            set
            {
                _Revision = value;
            }
        }
        public short MajorRevision
        {
            get
            {
                return (short)(_Revision >> 16);
            }
        }
        public short MinorRevision
        {
            get
            {
                return (short)(_Revision & 0xFFFF);
            }
        }
        public VersionEx(int major, int minor, int build, int revision)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major");
            }

            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor");
            }

            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build");
            }

            if (revision < 0)
            {
                throw new ArgumentOutOfRangeException("revision");
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
            _Revision = revision;
        }
        public VersionEx(int major, int minor, int build)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major");
            }

            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor");
            }

            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build");
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
        }
        public VersionEx(int major, int minor)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major");
            }

            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor");
            }

            _Major = major;
            _Minor = minor;
        }
        public VersionEx(string version)
        {
            VersionEx version2 = Parse(version);
            _Major = version2.Major;
            _Minor = version2.Minor;
            _Build = version2.Build;
            _Revision = version2.Revision;
        }
        public VersionEx()
        {
            _Major = 0;
            _Minor = 0;
        }
        public object Clone()
        {
            VersionEx version = new VersionEx();
            version._Major = _Major;
            version._Minor = _Minor;
            version._Build = _Build;
            version._Revision = _Revision;
            return version;
        }
        public int CompareTo(object version)
        {
            if (version == null)
            {
                return 1;
            }

            VersionEx version2 = version as VersionEx;
            if (version2 == null)
            {
                throw new ArgumentException();
            }

            if (_Major != version2._Major)
            {
                if (_Major > version2._Major)
                {
                    return 1;
                }

                return -1;
            }

            if (_Minor != version2._Minor)
            {
                if (_Minor > version2._Minor)
                {
                    return 1;
                }

                return -1;
            }

            if (_Build != version2._Build)
            {
                if (_Build > version2._Build)
                {
                    return 1;
                }

                return -1;
            }

            if (_Revision != version2._Revision)
            {
                if (_Revision > version2._Revision)
                {
                    return 1;
                }

                return -1;
            }

            return 0;
        }
        public int CompareTo(VersionEx value)
        {
            if (value == null)
            {
                return 1;
            }

            if (_Major != value._Major)
            {
                if (_Major > value._Major)
                {
                    return 1;
                }

                return -1;
            }

            if (_Minor != value._Minor)
            {
                if (_Minor > value._Minor)
                {
                    return 1;
                }

                return -1;
            }

            if (_Build != value._Build)
            {
                if (_Build > value._Build)
                {
                    return 1;
                }

                return -1;
            }

            if (_Revision != value._Revision)
            {
                if (_Revision > value._Revision)
                {
                    return 1;
                }

                return -1;
            }

            return 0;
        }
        public override bool Equals(object obj)
        {
            VersionEx version = obj as VersionEx;
            if (version == null)
            {
                return false;
            }

            if (_Major != version._Major || _Minor != version._Minor || _Build != version._Build || _Revision != version._Revision)
            {
                return false;
            }

            return true;
        }
        public bool Equals(VersionEx obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (_Major != obj._Major || _Minor != obj._Minor || _Build != obj._Build || _Revision != obj._Revision)
            {
                return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            int num = 0;
            num |= (_Major & 0xF) << 28;
            num |= (_Minor & 0xFF) << 20;
            num |= (_Build & 0xFF) << 12;
            return num | (_Revision & 0xFFF);
        }
        public override string ToString()
        {
            if (_Build == -1)
            {
                return ToString(2);
            }

            if (_Revision == -1)
            {
                return ToString(3);
            }

            return ToString(4);
        }
        public string ToString(int fieldCount)
        {
            switch (fieldCount)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return _Major.ToString();
                case 2:
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        AppendPositiveNumber(_Major, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Minor, stringBuilder);
                        return stringBuilder.ToString();
                    }
                default:
                    if (_Build == -1)
                    {
                        throw new ArgumentException();
                    }

                    if (fieldCount == 3)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        AppendPositiveNumber(_Major, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Minor, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Build, stringBuilder);
                        return stringBuilder.ToString();
                    }

                    if (_Revision == -1)
                    {
                        throw new ArgumentException();
                    }

                    if (fieldCount == 4)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        AppendPositiveNumber(_Major, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Minor, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Build, stringBuilder);
                        stringBuilder.Append('.');
                        AppendPositiveNumber(_Revision, stringBuilder);
                        return stringBuilder.ToString();
                    }

                    throw new ArgumentException();
            }
        }
        private static void AppendPositiveNumber(int num, StringBuilder sb)
        {
            int length = sb.Length;
            do
            {
                int num2 = num % 10;
                num /= 10;
                sb.Insert(length, (char)(48 + num2));
            }
            while (num > 0);
        }
        public static VersionEx Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            VersionResult result = default(VersionResult);
            result.Init("input", canThrow: true);
            if (!TryParseVersion(input, ref result))
            {
                throw result.GetVersionParseException();
            }

            return result.m_parsedVersion;
        }
        public static bool TryParse(string input, out VersionEx result)
        {
            VersionResult result2 = default(VersionResult);
            result2.Init("input", canThrow: false);
            bool result3 = TryParseVersion(input, ref result2);
            result = result2.m_parsedVersion;
            return result3;
        }
        private static bool TryParseVersion(string version, ref VersionResult result)
        {
            if (version == null)
            {
                result.SetFailure(ParseFailureKind.ArgumentNullException);
                return false;
            }

            string[] array = version.Split(SeparatorsArray);
            int num = array.Length;
            if (num < 2 || num > 4)
            {
                result.SetFailure(ParseFailureKind.ArgumentException);
                return false;
            }

            if (!TryParseComponent(array[0], "version", ref result, out int parsedComponent))
            {
                return false;
            }

            if (!TryParseComponent(array[1], "version", ref result, out int parsedComponent2))
            {
                return false;
            }

            num -= 2;
            if (num > 0)
            {
                if (!TryParseComponent(array[2], "build", ref result, out int parsedComponent3))
                {
                    return false;
                }

                num--;
                if (num > 0)
                {
                    if (!TryParseComponent(array[3], "revision", ref result, out int parsedComponent4))
                    {
                        return false;
                    }

                    result.m_parsedVersion = new VersionEx(parsedComponent, parsedComponent2, parsedComponent3, parsedComponent4);
                }
                else
                {
                    result.m_parsedVersion = new VersionEx(parsedComponent, parsedComponent2, parsedComponent3);
                }
            }
            else
            {
                result.m_parsedVersion = new VersionEx(parsedComponent, parsedComponent2);
            }

            return true;
        }
        private static bool TryParseComponent(string component, string componentName, ref VersionResult result, out int parsedComponent)
        {
            if (!int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent))
            {
                result.SetFailure(ParseFailureKind.FormatException, component);
                return false;
            }

            if (parsedComponent < 0)
            {
                result.SetFailure(ParseFailureKind.ArgumentOutOfRangeException, componentName);
                return false;
            }

            return true;
        }
        public static bool operator ==(VersionEx v1, VersionEx v2)
        {
            return v1?.Equals(v2) ?? ((object)v2 == null);
        }
        public static bool operator !=(VersionEx v1, VersionEx v2)
        {
            return !(v1 == v2);
        }
        public static bool operator <(VersionEx v1, VersionEx v2)
        {
            if ((object)v1 == null)
            {
                throw new ArgumentNullException("v1");
            }

            return v1.CompareTo(v2) < 0;
        }
        public static bool operator <=(VersionEx v1, VersionEx v2)
        {
            if ((object)v1 == null)
            {
                throw new ArgumentNullException("v1");
            }

            return v1.CompareTo(v2) <= 0;
        }
        public static bool operator >(VersionEx v1, VersionEx v2)
        {
            return v2 < v1;
        }
        public static bool operator >=(VersionEx v1, VersionEx v2)
        {
            return v2 <= v1;
        }
    }
}
