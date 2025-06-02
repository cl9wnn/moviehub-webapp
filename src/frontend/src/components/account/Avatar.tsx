import React from "react";

type AvatarProps = {
  photoUrl?: string;
  name?: string;
};

const Avatar: React.FC<AvatarProps> = ({ photoUrl, name }) => (
  <div className="w-full max-w-[250px] ml-10">
    <div className="aspect-square overflow-hidden rounded-xl"> {/* Ключевое изменение */}
      <img
        src={`http://${photoUrl}`}
        alt={name}
        className="w-full h-full object-cover"
      />
    </div>
  </div>
);

export default Avatar;