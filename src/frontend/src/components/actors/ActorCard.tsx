import React from "react";
import type { ActorCardResponse } from "../../models/movie.ts";
import { useNavigate } from "react-router-dom";

interface ActorCardProps {
  actor: ActorCardResponse;
}

const ActorCard: React.FC<ActorCardProps> = ({ actor }) => {
  const navigate = useNavigate();

  const handleClick = (): void => {
    navigate(`/actors/${actor.id}`);
  }

  return (
    <div
      onClick={handleClick}
      className="min-w-[210px] text-center cursor-pointer"
    >
      <div className="relative w-full pt-[135%] overflow-hidden rounded-lg">
        <img
          src={`http://${actor.photoUrl}`}
          alt={`${actor.firstName} ${actor.lastName}`}
          className="absolute top-0 left-0 w-full h-full object-cover"
        />
      </div>
      <div className="mt-3 text-base font-semibold">
        {actor.firstName} {actor.lastName}
      </div>
      <div className="text-sm text-gray-600">{actor.characterName}</div>
    </div>
  );
};

export default ActorCard;