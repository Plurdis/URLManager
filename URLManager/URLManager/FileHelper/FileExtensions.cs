using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.FileHelper
{

    /// <summary>
    /// 정의된 파일 확장자들을 모두 가져옵니다.
    /// </summary>
    public enum FileExtensions
    {
        #region A로 시작하는 파일 확장자

        [Description("MPEG-2 음악")]
        AAC,
        [Description("AVI 동영상")]
        AVI,
        [Description("알집 전용 압축")]
        ALZ,
        [Description("어도비 일러스트레이터")]
        AI,
        [Description("안드로이드 어플리케이션 패키지")]
        APK,

        #endregion

        #region B로 시작하는 파일 확장자

        [Description("백업된")]
        BAK,
        [Description("배치")]
        BAT,
        [Description("비트맵 그림")]
        BMP,

        #endregion

        #region C로 시작하는 파일 확장자

        [Description("C언어")]
        C,
        [Description("MS-DOS 실행")]
        COM,

        #endregion

        #region D로 시작하는 파일 확장자

        [Description("데이터")]
        DAT,
        [Description("응용 프로그램 동적 확장 라이브러리")]
        DLL,
        [Description("MS 워드 2007 이전 버전 문서")]
        DOC,
        [Description("MS 워드 2007 문서")]
        DOCX,
        [Description("Auto CAD 도면")]
        DWG,

        #endregion

        #region E로 시작하는 파일 확장자

        [Description("알집 전용 압축")]
        EGG,
        [Description("실행")]
        EXE,

        #endregion

        #region F로 시작하는 파일 확장자

        [Description("플래시 동영상")]
        FLV,

        #endregion

        #region G로 시작하는 파일 확장자

        [Description("움직일 수 있는 그림")]
        GIF,

        #endregion

        #region H로 시작하는 파일 확장자

        [Description("웹 문서")]
        HTM,
        [Description("웹 문서")]
        HTML,
        [Description("아래아 한글용 문서")]
        HWP,

        #endregion

        #region I로 시작하는 파일 확장자

        [Description("아이콘")]
        ICO,
        [Description("설정")]
        INI,
        [Description("CD/DVD 표준 이미지")]
        ISO,

        #endregion

        #region J로 시작하는 파일 확장자

        [Description("자바 관련")]
        JAR,
        [Description("자바 언어 소스")]
        JAVA,
        [Description("그림")]
        JPEG,
        [Description("그림")]
        JPG,
        [Description("자바 스크립트")]
        JS,
        [Description("자바 서버 페이지")]
        JSP,
        [Description("JSON 포맷 자료 전달")]
        JSON,

        #endregion

        #region K로 시작하는 파일 확장자

        [Description("피쳐폰용 동영상")]
        K3G,

        #endregion

        #region L로 시작하는 파일 확장자

        [Description("바로 가기")]
        LNK,
        [Description("이벤트 기록")]
        LOG,
        [Description("싱크 가사")]
        LRC,

        #endregion

        #region M으로 시작하는 파일 확장자

        [Description("MP2 음악")]
        MP2,
        [Description("MP3 음악")]
        MP3,
        [Description("MP4 멀티미디어")]
        MP4,
        [Description("MPEG 멀티미디어")]
        MPEG,
        [Description("MPG 멀티미디어")]
        MPG,
        [Description("Windows 설치/제거 프로그램")]
        MSI,

        #endregion

        #region N으로 시작하는 파일 확장자

        [Description("Nintendo DS 롬")]
        NDS,

        #endregion

        #region O로 시작하는 파일 확장자

        [Description("C언어 오브젝트")]
        O,
        [Description("OGG 멀티미디어")]
        OGG,
        [Description("OSU 비트맵")]
        OSU,
        [Description("OSU 비트맵 세트")]
        OSZ,

        #endregion

        #region P로 시작하는 파일 확장자

        [Description("파워포인트 2007 이전 버전 문서")]
        PPT,
        [Description("파워포인트 2007 문서")]
        PPTX,
        [Description("PHP 소스")]
        PHP,
        [Description("PNG 그림")]
        PNG,

        #endregion

        #region Q로 시작하는 파일 확장자 (비어있음)
        #endregion

        #region R로 시작하는 파일 확장자 (비어있음)

        [Description("RAR 압축")]
        RAR,

        #endregion

        #region S로 시작하는 파일 확장자

        [Description("플래시 동영상")]
        SWF,

        #endregion

        #region T로 시작하는 파일 확장자

        [Description("텍스트 문서")]
        TXT,

        #endregion

        #region U로 시작하는 파일 확장자 (비어있음)
        #endregion

        #region V로 시작하는 파일 확장자 (비어있음)
        #endregion

        #region W로 시작하는 파일 확장자

        [Description("WMV 동영상")]
        WMV,
        [Description("WMA 음악")]
        WMA,
        [Description("WAV 음악")]
        WAV,

        #endregion

        #region X로 시작하는 파일 확장자 (비어있음)
        #endregion

        #region Y로 시작하는 파일 확장자 (비어있음)
        #endregion

        #region Z로 시작하는 파일 확장자

        [Description("ZIP 압축")]
        ZIP,
        [Description("ZIP 개선 압축")]
        ZIPX

        #endregion
    }
}
