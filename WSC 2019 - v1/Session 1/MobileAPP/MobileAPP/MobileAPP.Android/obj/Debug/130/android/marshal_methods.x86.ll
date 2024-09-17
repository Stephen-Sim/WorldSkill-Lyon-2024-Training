; ModuleID = 'obj\Debug\130\android\marshal_methods.x86.ll'
source_filename = "obj\Debug\130\android\marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [212 x i32] [
	i32 22737483, ; 0: MobileAPP.Android => 0x15af24b => 0
	i32 32687329, ; 1: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 46
	i32 34715100, ; 2: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 75
	i32 39109920, ; 3: Newtonsoft.Json.dll => 0x254c520 => 9
	i32 57263871, ; 4: Xamarin.Forms.Core.dll => 0x369c6ff => 70
	i32 101534019, ; 5: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 60
	i32 120558881, ; 6: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 60
	i32 165246403, ; 7: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 27
	i32 182336117, ; 8: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 61
	i32 209399409, ; 9: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 25
	i32 220171995, ; 10: System.Diagnostics.Debug => 0xd1f8edb => 96
	i32 230216969, ; 11: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 41
	i32 230752869, ; 12: Microsoft.CSharp.dll => 0xdc10265 => 104
	i32 231814094, ; 13: System.Globalization => 0xdd133ce => 90
	i32 232815796, ; 14: System.Web.Services => 0xde07cb4 => 85
	i32 261689757, ; 15: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 30
	i32 278686392, ; 16: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 45
	i32 280482487, ; 17: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 39
	i32 318968648, ; 18: Xamarin.AndroidX.Activity.dll => 0x13031348 => 17
	i32 321597661, ; 19: System.Numerics => 0x132b30dd => 12
	i32 342366114, ; 20: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 43
	i32 441335492, ; 21: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 29
	i32 442521989, ; 22: Xamarin.Essentials => 0x1a605985 => 69
	i32 442565967, ; 23: System.Collections => 0x1a61054f => 89
	i32 450948140, ; 24: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 38
	i32 459347974, ; 25: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 91
	i32 465846621, ; 26: mscorlib => 0x1bc4415d => 8
	i32 469710990, ; 27: System.dll => 0x1bff388e => 11
	i32 476646585, ; 28: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 39
	i32 486930444, ; 29: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 50
	i32 498788369, ; 30: System.ObjectModel => 0x1dbae811 => 97
	i32 522460022, ; 31: MobileAPP.dll => 0x1f241b76 => 6
	i32 526420162, ; 32: System.Transactions.dll => 0x1f6088c2 => 79
	i32 545304856, ; 33: System.Runtime.Extensions => 0x2080b118 => 101
	i32 605376203, ; 34: System.IO.Compression.FileSystem => 0x24154ecb => 83
	i32 627609679, ; 35: Xamarin.AndroidX.CustomView => 0x2568904f => 34
	i32 663517072, ; 36: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 66
	i32 666292255, ; 37: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 22
	i32 690569205, ; 38: System.Xml.Linq.dll => 0x29293ff5 => 16
	i32 775507847, ; 39: System.IO.Compression => 0x2e394f87 => 82
	i32 809851609, ; 40: System.Drawing.Common.dll => 0x30455ad9 => 81
	i32 843511501, ; 41: Xamarin.AndroidX.Print => 0x3246f6cd => 57
	i32 877678880, ; 42: System.Globalization.dll => 0x34505120 => 90
	i32 928116545, ; 43: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 75
	i32 941273916, ; 44: MobileAPP => 0x381ab33c => 6
	i32 955402788, ; 45: Newtonsoft.Json => 0x38f24a24 => 9
	i32 967690846, ; 46: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 43
	i32 974778368, ; 47: FormsViewGroup.dll => 0x3a19f000 => 4
	i32 975874589, ; 48: System.Xml.XDocument => 0x3a2aaa1d => 88
	i32 992768348, ; 49: System.Collections.dll => 0x3b2c715c => 89
	i32 1012816738, ; 50: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 59
	i32 1035644815, ; 51: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 21
	i32 1042160112, ; 52: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 72
	i32 1044663988, ; 53: System.Linq.Expressions.dll => 0x3e444eb4 => 93
	i32 1052210849, ; 54: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 47
	i32 1098259244, ; 55: System => 0x41761b2c => 11
	i32 1175144683, ; 56: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 64
	i32 1178241025, ; 57: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 54
	i32 1204270330, ; 58: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 22
	i32 1267360935, ; 59: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 65
	i32 1293217323, ; 60: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 36
	i32 1324164729, ; 61: System.Linq => 0x4eed2679 => 95
	i32 1364015309, ; 62: System.IO => 0x514d38cd => 86
	i32 1365406463, ; 63: System.ServiceModel.Internals.dll => 0x516272ff => 76
	i32 1376866003, ; 64: Xamarin.AndroidX.SavedState => 0x52114ed3 => 59
	i32 1395857551, ; 65: Xamarin.AndroidX.Media.dll => 0x5333188f => 51
	i32 1406073936, ; 66: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 31
	i32 1457743152, ; 67: System.Runtime.Extensions.dll => 0x56e36530 => 101
	i32 1460219004, ; 68: Xamarin.Forms.Xaml => 0x57092c7c => 73
	i32 1462112819, ; 69: System.IO.Compression.dll => 0x57261233 => 82
	i32 1469204771, ; 70: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 20
	i32 1540334473, ; 71: MobileAPP.Android.dll => 0x5bcfa389 => 0
	i32 1543031311, ; 72: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 98
	i32 1550322496, ; 73: System.Reflection.Extensions.dll => 0x5c680b40 => 1
	i32 1582372066, ; 74: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 35
	i32 1592978981, ; 75: System.Runtime.Serialization.dll => 0x5ef2ee25 => 3
	i32 1622152042, ; 76: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 49
	i32 1622358360, ; 77: System.Dynamic.Runtime => 0x60b33958 => 94
	i32 1624863272, ; 78: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 68
	i32 1636350590, ; 79: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 33
	i32 1639515021, ; 80: System.Net.Http.dll => 0x61b9038d => 2
	i32 1639986890, ; 81: System.Text.RegularExpressions => 0x61c036ca => 98
	i32 1657153582, ; 82: System.Runtime => 0x62c6282e => 14
	i32 1658241508, ; 83: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 62
	i32 1658251792, ; 84: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 74
	i32 1670060433, ; 85: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 30
	i32 1701541528, ; 86: System.Diagnostics.Debug.dll => 0x656b7698 => 96
	i32 1726116996, ; 87: System.Reflection.dll => 0x66e27484 => 92
	i32 1729485958, ; 88: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 26
	i32 1765942094, ; 89: System.Reflection.Extensions => 0x6942234e => 1
	i32 1766324549, ; 90: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 61
	i32 1776026572, ; 91: System.Core.dll => 0x69dc03cc => 10
	i32 1788241197, ; 92: Xamarin.AndroidX.Fragment => 0x6a96652d => 38
	i32 1808609942, ; 93: Xamarin.AndroidX.Loader => 0x6bcd3296 => 49
	i32 1813201214, ; 94: Xamarin.Google.Android.Material => 0x6c13413e => 74
	i32 1818569960, ; 95: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 55
	i32 1824175904, ; 96: System.Text.Encoding.Extensions => 0x6cbab720 => 103
	i32 1858542181, ; 97: System.Linq.Expressions => 0x6ec71a65 => 93
	i32 1867746548, ; 98: Xamarin.Essentials.dll => 0x6f538cf4 => 69
	i32 1878053835, ; 99: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 73
	i32 1885316902, ; 100: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 23
	i32 1919157823, ; 101: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 52
	i32 2019465201, ; 102: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 47
	i32 2055257422, ; 103: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 44
	i32 2079903147, ; 104: System.Runtime.dll => 0x7bf8cdab => 14
	i32 2090596640, ; 105: System.Numerics.Vectors => 0x7c9bf920 => 13
	i32 2097448633, ; 106: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 40
	i32 2126786730, ; 107: Xamarin.Forms.Platform.Android => 0x7ec430aa => 71
	i32 2193016926, ; 108: System.ObjectModel.dll => 0x82b6c85e => 97
	i32 2201231467, ; 109: System.Net.Http => 0x8334206b => 2
	i32 2217644978, ; 110: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 64
	i32 2244775296, ; 111: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 50
	i32 2256548716, ; 112: Xamarin.AndroidX.MultiDex => 0x8680336c => 52
	i32 2261435625, ; 113: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 42
	i32 2279755925, ; 114: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 58
	i32 2315684594, ; 115: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 18
	i32 2368005991, ; 116: System.Xml.ReaderWriter.dll => 0x8d24e767 => 99
	i32 2409053734, ; 117: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 56
	i32 2454642406, ; 118: System.Text.Encoding.dll => 0x924edee6 => 100
	i32 2465532216, ; 119: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 29
	i32 2471841756, ; 120: netstandard.dll => 0x93554fdc => 77
	i32 2475788418, ; 121: Java.Interop.dll => 0x93918882 => 5
	i32 2501346920, ; 122: System.Data.DataSetExtensions => 0x95178668 => 80
	i32 2505896520, ; 123: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 46
	i32 2562349572, ; 124: Microsoft.CSharp => 0x98ba5a04 => 104
	i32 2581819634, ; 125: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 65
	i32 2585220780, ; 126: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 103
	i32 2620871830, ; 127: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 33
	i32 2624644809, ; 128: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 37
	i32 2633051222, ; 129: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 45
	i32 2664396074, ; 130: System.Xml.XDocument.dll => 0x9ecf752a => 88
	i32 2693849962, ; 131: System.IO.dll => 0xa090e36a => 86
	i32 2701096212, ; 132: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 62
	i32 2715334215, ; 133: System.Threading.Tasks.dll => 0xa1d8b647 => 87
	i32 2732626843, ; 134: Xamarin.AndroidX.Activity => 0xa2e0939b => 17
	i32 2737747696, ; 135: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 20
	i32 2766581644, ; 136: Xamarin.Forms.Core => 0xa4e6af8c => 70
	i32 2778768386, ; 137: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 67
	i32 2810250172, ; 138: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 31
	i32 2819470561, ; 139: System.Xml.dll => 0xa80db4e1 => 15
	i32 2853208004, ; 140: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 67
	i32 2855708567, ; 141: Xamarin.AndroidX.Transition => 0xaa36a797 => 63
	i32 2900621748, ; 142: System.Dynamic.Runtime.dll => 0xace3f9b4 => 94
	i32 2901442782, ; 143: System.Reflection => 0xacf080de => 92
	i32 2903344695, ; 144: System.ComponentModel.Composition => 0xad0d8637 => 84
	i32 2905242038, ; 145: mscorlib.dll => 0xad2a79b6 => 8
	i32 2916838712, ; 146: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 68
	i32 2919462931, ; 147: System.Numerics.Vectors.dll => 0xae037813 => 13
	i32 2921128767, ; 148: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 19
	i32 2978675010, ; 149: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 36
	i32 3024354802, ; 150: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 41
	i32 3044182254, ; 151: FormsViewGroup => 0xb57288ee => 4
	i32 3057625584, ; 152: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 53
	i32 3075834255, ; 153: System.Threading.Tasks => 0xb755818f => 87
	i32 3111772706, ; 154: System.Runtime.Serialization => 0xb979e222 => 3
	i32 3204380047, ; 155: System.Data.dll => 0xbefef58f => 78
	i32 3211777861, ; 156: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 35
	i32 3220365878, ; 157: System.Threading => 0xbff2e236 => 102
	i32 3247949154, ; 158: Mono.Security => 0xc197c562 => 105
	i32 3258312781, ; 159: Xamarin.AndroidX.CardView => 0xc235e84d => 26
	i32 3267021929, ; 160: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 24
	i32 3299363146, ; 161: System.Text.Encoding => 0xc4a8494a => 100
	i32 3317135071, ; 162: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 34
	i32 3317144872, ; 163: System.Data => 0xc5b79d28 => 78
	i32 3340431453, ; 164: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 23
	i32 3346324047, ; 165: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 54
	i32 3353484488, ; 166: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 40
	i32 3362522851, ; 167: Xamarin.AndroidX.Core => 0xc86c06e3 => 32
	i32 3366347497, ; 168: Java.Interop => 0xc8a662e9 => 5
	i32 3374999561, ; 169: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 58
	i32 3404865022, ; 170: System.ServiceModel.Internals => 0xcaf21dfe => 76
	i32 3429136800, ; 171: System.Xml => 0xcc6479a0 => 15
	i32 3430777524, ; 172: netstandard => 0xcc7d82b4 => 77
	i32 3441283291, ; 173: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 37
	i32 3476120550, ; 174: Mono.Android => 0xcf3163e6 => 7
	i32 3486566296, ; 175: System.Transactions => 0xcfd0c798 => 79
	i32 3493954962, ; 176: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 28
	i32 3501239056, ; 177: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 24
	i32 3509114376, ; 178: System.Xml.Linq => 0xd128d608 => 16
	i32 3536029504, ; 179: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 71
	i32 3567349600, ; 180: System.ComponentModel.Composition.dll => 0xd4a16f60 => 84
	i32 3608519521, ; 181: System.Linq.dll => 0xd715a361 => 95
	i32 3618140916, ; 182: Xamarin.AndroidX.Preference => 0xd7a872f4 => 56
	i32 3627220390, ; 183: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 57
	i32 3632359727, ; 184: Xamarin.Forms.Platform => 0xd881692f => 72
	i32 3633644679, ; 185: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 19
	i32 3641597786, ; 186: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 44
	i32 3672681054, ; 187: Mono.Android.dll => 0xdae8aa5e => 7
	i32 3676310014, ; 188: System.Web.Services.dll => 0xdb2009fe => 85
	i32 3682565725, ; 189: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 25
	i32 3684561358, ; 190: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 28
	i32 3689375977, ; 191: System.Drawing.Common => 0xdbe768e9 => 81
	i32 3718780102, ; 192: Xamarin.AndroidX.Annotation => 0xdda814c6 => 18
	i32 3724971120, ; 193: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 53
	i32 3758932259, ; 194: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 42
	i32 3786282454, ; 195: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 27
	i32 3822602673, ; 196: Xamarin.AndroidX.Media => 0xe3d849b1 => 51
	i32 3829621856, ; 197: System.Numerics.dll => 0xe4436460 => 12
	i32 3885922214, ; 198: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 63
	i32 3896760992, ; 199: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 32
	i32 3920810846, ; 200: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 83
	i32 3921031405, ; 201: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 66
	i32 3928044579, ; 202: System.Xml.ReaderWriter => 0xea213423 => 99
	i32 3931092270, ; 203: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 55
	i32 3945713374, ; 204: System.Data.DataSetExtensions.dll => 0xeb2ecede => 80
	i32 3955647286, ; 205: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 21
	i32 4073602200, ; 206: System.Threading.dll => 0xf2ce3c98 => 102
	i32 4105002889, ; 207: Mono.Security.dll => 0xf4ad5f89 => 105
	i32 4151237749, ; 208: System.Core => 0xf76edc75 => 10
	i32 4181436372, ; 209: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 91
	i32 4182413190, ; 210: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 48
	i32 4292120959 ; 211: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 48
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [212 x i32] [
	i32 0, i32 46, i32 75, i32 9, i32 70, i32 60, i32 60, i32 27, ; 0..7
	i32 61, i32 25, i32 96, i32 41, i32 104, i32 90, i32 85, i32 30, ; 8..15
	i32 45, i32 39, i32 17, i32 12, i32 43, i32 29, i32 69, i32 89, ; 16..23
	i32 38, i32 91, i32 8, i32 11, i32 39, i32 50, i32 97, i32 6, ; 24..31
	i32 79, i32 101, i32 83, i32 34, i32 66, i32 22, i32 16, i32 82, ; 32..39
	i32 81, i32 57, i32 90, i32 75, i32 6, i32 9, i32 43, i32 4, ; 40..47
	i32 88, i32 89, i32 59, i32 21, i32 72, i32 93, i32 47, i32 11, ; 48..55
	i32 64, i32 54, i32 22, i32 65, i32 36, i32 95, i32 86, i32 76, ; 56..63
	i32 59, i32 51, i32 31, i32 101, i32 73, i32 82, i32 20, i32 0, ; 64..71
	i32 98, i32 1, i32 35, i32 3, i32 49, i32 94, i32 68, i32 33, ; 72..79
	i32 2, i32 98, i32 14, i32 62, i32 74, i32 30, i32 96, i32 92, ; 80..87
	i32 26, i32 1, i32 61, i32 10, i32 38, i32 49, i32 74, i32 55, ; 88..95
	i32 103, i32 93, i32 69, i32 73, i32 23, i32 52, i32 47, i32 44, ; 96..103
	i32 14, i32 13, i32 40, i32 71, i32 97, i32 2, i32 64, i32 50, ; 104..111
	i32 52, i32 42, i32 58, i32 18, i32 99, i32 56, i32 100, i32 29, ; 112..119
	i32 77, i32 5, i32 80, i32 46, i32 104, i32 65, i32 103, i32 33, ; 120..127
	i32 37, i32 45, i32 88, i32 86, i32 62, i32 87, i32 17, i32 20, ; 128..135
	i32 70, i32 67, i32 31, i32 15, i32 67, i32 63, i32 94, i32 92, ; 136..143
	i32 84, i32 8, i32 68, i32 13, i32 19, i32 36, i32 41, i32 4, ; 144..151
	i32 53, i32 87, i32 3, i32 78, i32 35, i32 102, i32 105, i32 26, ; 152..159
	i32 24, i32 100, i32 34, i32 78, i32 23, i32 54, i32 40, i32 32, ; 160..167
	i32 5, i32 58, i32 76, i32 15, i32 77, i32 37, i32 7, i32 79, ; 168..175
	i32 28, i32 24, i32 16, i32 71, i32 84, i32 95, i32 56, i32 57, ; 176..183
	i32 72, i32 19, i32 44, i32 7, i32 85, i32 25, i32 28, i32 81, ; 184..191
	i32 18, i32 53, i32 42, i32 27, i32 51, i32 12, i32 63, i32 32, ; 192..199
	i32 83, i32 66, i32 99, i32 55, i32 80, i32 21, i32 102, i32 105, ; 200..207
	i32 10, i32 91, i32 48, i32 48 ; 208..211
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"NumRegisterParameters", i32 0}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
