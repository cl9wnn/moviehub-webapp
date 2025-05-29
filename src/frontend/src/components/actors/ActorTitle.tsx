import React from "react";
import TagButton from "../common/TagButton.tsx";

type ActorTitleProps = {
  firstName: string;
  lastName: string;
}

const ActorTitle : React.FC<ActorTitleProps> = ({firstName, lastName}) => {
  return (
    <div className="space-y-4">
      <h1 className="text-5xl font-bold">{firstName + " " + lastName}</h1>
      <div className="flex gap-4 flex-wrap">
        <TagButton>Любимый актер</TagButton>
      </div>
    </div>
  );
};

export default ActorTitle;