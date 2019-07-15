sampler TextureSampler : register(s0);
float3 key_color;
float3 new_color;

float4 ChangePixel(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
  {
    float4 newColor = tex2D(TextureSampler, texCoord);

  if (distance(key_color, newColor.rgb)<0.001f)
  {
     newColor.rgb = new_color;
  } 

  return newColor* color;
} 

technique PixelChange
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 ChangePixel();
    }
}