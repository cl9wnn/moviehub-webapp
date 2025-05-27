import React from "react";
import TagButton from "../common/TagButton.tsx";

type MovieTitleProps = {
  title: string;
}

const MovieTitle : React.FC<MovieTitleProps> = ({title}) => {
  return (
    <div className="space-y-4">
      <h1 className="text-5xl font-bold">{title}</h1>
      <div className="flex gap-4 flex-wrap">
        <TagButton>Буду смотреть</TagButton>
        <TagButton>Неинтересно</TagButton>
      </div>
    </div>
  );
};

export default MovieTitle;