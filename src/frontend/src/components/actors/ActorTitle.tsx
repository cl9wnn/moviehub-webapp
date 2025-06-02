import React from "react";
import TagButton from "../common/TagButton.tsx";
import {Heart} from "lucide-react";

type ActorTitleProps = {
  firstName: string;
  lastName: string;
  isFavorite: boolean;
  onToggleFavorite: () => void;
}

const ActorTitle: React.FC<ActorTitleProps> = ({ firstName, lastName, isFavorite, onToggleFavorite }) => {
  return (
    <div className="space-y-4">
      <h1 className="text-5xl font-bold">{firstName + " " + lastName}</h1>
      <div className="flex gap-4 flex-wrap">
        <TagButton
          onClick={onToggleFavorite}
          className={
          isFavorite
            ? "bg-red-100 text-red-600 hover:bg-red-200"
            : "bg-gray-200 text-gray-800"
          }
        >
  <span className="flex items-center gap-2">
    Любимая звезда
     <Heart
       className={
         isFavorite
           ? "w-5 h-5 stroke-red-600 fill-red-600"
           : "w-5 h-5 stroke-gray-600 fill-none"
       }
     />
  </span>
        </TagButton>
      </div>
    </div>
  );
};

export default ActorTitle;