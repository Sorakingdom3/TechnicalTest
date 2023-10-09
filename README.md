### 1. How could we save the use of a texture on the shader of Task 4 and still maintain its functionality?

As I have done, using the lerp function to apply the mask to the diffuse texture, you can avoid using the mask by coding the shader itself to do so.
Therefore avoiding the use of two textures.

### 2. If we have 4 objects with different textures, but they all use the same shader, how could we render all 4 with just one Draw Call?
Draw calls to different textures can be avoided by creating atlases and treating them all as a unit. By creating this grouping, you can Draw them in one call and also allow better control of tiled textures when building a tilemap for 2D games.

### 3. We have to optimize a scene for a console that has 2GB RAM and the profiler shows the following usage data:
#### a. 1 million triangles per frame
#### b. 1000 draw calls per frame
#### c. 1.9 GB of used memory
### If you could only dedicate time to optimize one of these factors, which one would be and why?

I would start with c. Getting to nearly the maximum RAM allowed is dangerous as it can cause slow execution and performance issues. This can be caused by large textures being loaded, and not unloaded when not required, heavy processes that could be optimized.
Profiling is a must to ensure the game runs correctly. After that I would continue with b and a in this order. Calling too many times to unrequired methods or repeated ones, is a huge problem to performance. Finally, polygons should be controlled by LOD and if not enough, models should be trimmed to be less detailed or maybe it's because unseen items are also being loaded at full quality.
