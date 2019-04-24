key_left = keyboard_check(ord("A"));
key_right = keyboard_check(ord("D"));
key_jump = keyboard_check_pressed(vk_space);


// Movement

var move = key_right - key_left;

hsp = move * walksp;

vsp = vsp + grv;

// Gravity

if(place_meeting(x, y+1, obj_block)) && (key_jump)
{
	vsp = -7;
	audio_play_sound(snd_jump,1,0);
}

// Horizontal collisions
if(place_meeting(x+hsp,y,obj_block))
{
	while (!place_meeting(x+sign(hsp),y,obj_block))
	{
		x = x + sign(hsp);
	}
	hsp = 0;
}
x = x + hsp;

// Vertical collisions
if(place_meeting(x,y+vsp,obj_block))
{
	while (!place_meeting(x,y+sign(vsp),obj_block))
	{
		y = y + sign(vsp);
	}
	vsp = 0;
}
y = y + vsp;


//animation controller

if(hsp > 0)
{
	sprite_index = spr_player;
	image_xscale = 1;
}
else if(hsp < 0)
{
	sprite_index = spr_player;
	image_xscale = -1;
}
else 
{
	sprite_index = spr_player_idle;
}

if(hp <= 0)
{
	audio_play_sound(snd_game_over,1,0);
	instance_destroy();
	room_goto(Main_menu);
}



//control hp

if(hp < 0)
{
	hp = 0;
}
if(hp >= maxhp)
{
	hp = maxhp;
}

//invincible
if(image_blend == c_red)
{
	invincible = true;
}
else
{
	invincible = false;
}











