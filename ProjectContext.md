# ProjectContext.md — ConcreteStair Plugin
> Project-specific conventions for the ConcreteStair plugin.
> Always read alongside `TeklaContext.md`. Update this file as conventions evolve.

---

## 1. Class Numbers (Color Coding)

> [!IMPORTANT]
> If a member type is not listed here, **stop and ask the user** before assigning a Class value.
> Do not invent or reuse class numbers without explicit confirmation.

| Class | Member Type |
|---|---|
| `4` | Stair body (main concrete `Beam`) |
| `8` | Stringer (`ContourPlate`) |
| `11` | Pipe (`Beam` with pipe profile) |
| `"BlOpCl"` | Boolean operative part (use `BooleanPart.BooleanOperativeClassName`) |

---

## 2. Material Strings

Map member type to the correct ASTM material string. Use exactly as shown — Tekla catalog is case-sensitive.

| Member Type | `MaterialString` |
|---|---|
| Plates, Angles, Flat Bars, Channels, Rod | `"A36"` |
| HSS (rectangular/square tube) | `"A500-GR.B"` |
| Pipe | `"A53-GR.B"` |
| Concrete parts | `"Concrete_Undefined"` |
| Temporary boolean operatives (steel) | `"Steel_Undefined"` |
| Grating / Buy-out items | `"BUY-OUT"` |

---

## 3. Assembly Numbering Prefixes

Set these **before** calling `part.Insert()`.

| Assembly Type | `AssemblyNumber.Prefix` | `StartNumber` |
|---|---|---|
| Brace Angles | `"1A"` | `1` |
| Channels | `"1C"` | `1` |
| Columns | `"1CO"` | `1` |
| Posts | `"1PO"` | `1` |
| Plates | `"1P"` | `1` |
| Handrails | `"1HR"` | `1` |
| Rails | `"1R"` | `1` |
| Stringers | `"1STR"` | `1` |
| Grating & Treads | `"1GRT"` | `1` |

---

## 4. Part Numbering Prefixes

| Part Type | `PartNumber.Prefix` | `StartNumber` |
|---|---|---|
| Angles | `"a1"` | `1` |
| Plates & Flat Bars | `"p1"` | `1` |
| Base Plates | `"bp1"` | `1` |
| Balusters | `"b1"` | `1` |
| Brackets | `"br1"` | `1` |
| Channels | `"c1"` | `1` |
| Pipes | `"r1"` | `1` |
| Handrail Pipes | `"hr1"` | `1` |

> [!NOTE]
> The `xx` in the numbering scheme (e.g. `a1xx`) is handled by Tekla automatically via `StartNumber`.
> Set `StartNumber = 1` unless a specific offset is required for the project.

---

## 5. Profile String Conventions

```
Rectangular beam:   "HEIGHTxWIDTH"         e.g. "400X1200"
Plate:              "PLThickness"            e.g. "PL200"  (thickness in mm)
Pipe:               "PIPESize-Schedule"      e.g. "PIPE1-1/2STD"
HSS:                "HSSHxWxT"              e.g. "HSS4X4X1/4"
Angle:              "LHxWxT"                e.g. "L3X3X1/4"
Channel:            "CDepthxWeight"          e.g. "C6X8.2"
```

---

## 6. ConcreteStair Geometry Reference

For agents extending this plugin, here is the geometric model:

```
StartPoint ──────────────────────── EndPoint  (user-picked, global XY)
│                                          │
│   Run = total horizontal travel          │
│   Rise = total vertical travel           │
│   Landing = flat extension at top        │
│   NumberOfTreads = tread count           │
│   FirstRiser = height of first step      │
│                                          │
│   treadRunLength  = Run / NumberOfTreads
│   treadRiseHeight = (Rise - FirstRiser) / NumberOfTreads
```

**Main Body:** A `Beam` from `StartPoint` to `StartPoint + (Run + Landing)` along `runDirection`.  
Profile is `Rise x StairWidth` (Height × Width). `Position.Depth = FRONT`.

**Tread Cutout:** A `ContourPlate` polygon tracing the stair step profile from outside the front face to outside the back of the landing. Applied as `BOOLEAN_CUT` to the main body.

**Soffit Cutout:** A `ContourPlate` polygon removing material below the sloped soffit. Throat thickness (`TreadThickness`) is the **perpendicular** distance, not vertical — use:
```csharp
double slopeLength  = Math.Sqrt(treadRunLength² + treadRiseHeight²);
double cosTheta     = treadRunLength / slopeLength;
double verticalDrop = TreadThickness / cosTheta;  // converts perpendicular → vertical
```

**Stringers:** `ContourPlate` members. Profile 2D points are computed in the stair's local run-direction frame, then projected to 3D using `runDir` and `lateralDir` vectors.

**Alignment:**
| `Alignment` | `Position.Plane` | Left stringer offset | Right stringer offset |
|---|---|---|---|
| `"Left"` | `PlaneEnum.LEFT` | `StairWidth` | `0` |
| `"Right"` | `PlaneEnum.RIGHT` | `0` | `StairWidth` |
| `"Middle"` | `PlaneEnum.MIDDLE` | `StairWidth / 2` | `StairWidth / 2` |

**Nosing chamfer:** `Chamfer.ChamferTypeEnum.CHAMFER_ROUNDING` with radius `NosingRadius`. Pass `null` for sharp inner corners.

**Overshoot:** Add `10.0.ToMm()` (10 mm) to cut polygons on all open boundaries to avoid Tekla micro-face rendering artifacts.

---

## 7. Build Order

Always insert in this order to avoid dependency errors:

1. Main stair body (`Beam`) — `Insert()`
2. Tread cutout (`ContourPlate` + `BooleanPart`) — `Insert()`, then `Delete()` operative
3. Soffit cutout (`ContourPlate` + `BooleanPart`) — `Insert()`, then `Delete()` operative
4. Stringers (`ContourPlate`) — `Insert()`
5. `_model.CommitChanges()` — once, after all parts are inserted

